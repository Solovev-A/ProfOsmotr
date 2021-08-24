using ProfOsmotr.BL.Abstractions;
using ProfOsmotr.DAL;
using ProfOsmotr.DAL.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfOsmotr.BL
{
    public class CalculationService : ICalculationService
    {
        #region Fields

        private readonly ICalculationSourceFactory calculationSourceFactory;
        private readonly ICatalogService catalogService;
        private readonly IOrderService orderService;
        private readonly IProfCalculator profCalculator;
        private readonly IProfUnitOfWork uow;

        #endregion Fields

        #region Constructors

        public CalculationService(ICalculationSourceFactory calculationSourceFactory,
                                  ICatalogService catalogService,
                                  IOrderService orderService,
                                  IProfCalculator profCalculator,
                                  IProfUnitOfWork uow)
        {
            this.calculationSourceFactory = calculationSourceFactory ?? throw new ArgumentNullException(nameof(calculationSourceFactory));
            this.catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            this.profCalculator = profCalculator ?? throw new ArgumentNullException(nameof(profCalculator));
            this.uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        #endregion Constructors

        #region Methods

        public async Task<CalculationResponse> DeleteAsync(int id)
        {
            var calculation = await FindCalculationAsync(id);
            if (calculation == null)
                return new CalculationResponse("Расчет не найден");

            calculation.IsDeleted = true;

            try
            {
                await uow.SaveAsync();
                return new CalculationResponse(calculation);
            }
            catch (Exception ex)
            {
                return new CalculationResponse($"Во время удаления расчета произошла непредвиденная ошибка: {ex.Message}");
            }
        }

        public async Task<CalculationResponse> GetCalculation(int id)
        {
            var calculation = await FindCalculationAsync(id);
            if (calculation == null)
                return new CalculationResponse("Расчет не найден");
            return new CalculationResponse(calculation);
        }

        public async Task<PaginatedResult<Calculation>> ListAsync(CalculationsPaginationQuery query)
        {
            PaginatedResult<Calculation> result = await uow.Calculations.ListAsync(query);
            return result;
        }

        public async Task<CalculationResponse> MakeCalculation(CreateCalculationRequest request)
        {
            if (request is null)
                return new CalculationResponse("Запрос не может быть пустым");
            if (!request.CreateCalculationSourceRequests.Any())
                return new CalculationResponse("Для расчета необходимо добавить хотя бы одну профессию");

            Clinic clinic = await uow.Clinics.FindAsync(request.ClinicId);
            if (clinic == null)
                return new CalculationResponse("Клиника не найдена");

            User user = await FindUserAsync(request.UserId);
            if (user == null)
                return new CalculationResponse("Пользователь не найден");

            var sourceResponse = await calculationSourceFactory.CreateCalculationSources(request);
            if (!sourceResponse.Succeed)
            {
                return new CalculationResponse(sourceResponse.Message);
            }

            try
            {
                var calculation = await CreateCalculationAsync(sourceResponse.Result, request, user, clinic);
                await uow.Calculations.AddAsync(calculation);
                await uow.SaveAsync();
                return new CalculationResponse(calculation);
            }
            catch (Exception ex)
            {
                return new CalculationResponse($"При создании расчета произошла непредвиденная ошибка: {ex.Message}");
            }
        }

        public async Task<CalculationResponse> UpdateCalculationAsync(UpdateCalculationRequest request)
        {
            if (request is null)
                return new CalculationResponse("Запрос не может быть пустым");
            var creator = await FindUserAsync(request.CreatorId);
            if (creator == null)
                return new CalculationResponse("Пользователь не найден");
            var oldCalculation = await FindCalculationAsync(request.CalculationId);
            if (oldCalculation == null)
                return new CalculationResponse("Расчет не найден");
            if (oldCalculation.CalculationResultItems.Count != request.ResultItems.Count())
                return new CalculationResponse("Состав результата расчета нельзя изменить");

            var newCalculation = ModifyCalculation(oldCalculation);

            foreach (var newItemData in request.ResultItems)
            {
                var oldItem = oldCalculation.CalculationResultItems.FirstOrDefault(i => i.Id == newItemData.Id); // возможно, избыточно
                if (oldItem == null)
                    return new CalculationResponse($"Невозможно добавить в результат новый элемент (id {newItemData.Id})");
                if (!Enum.IsDefined(typeof(ServiceAvailabilityGroupId), newItemData.GroupId))
                    return new CalculationResponse($"Элемент #{newItemData.Id} содержит неверный номер группы: {newItemData.GroupId}");
                if (newItemData.Price < 0)
                    return new CalculationResponse($"Цена элемента #{newItemData.Id} оказалась отрицательной");
                if (newItemData.Amount < 0)
                    return new CalculationResponse($"Количество элемента #{newItemData.Id} оказалось отрицательным");

                var newItem = await UpdateResultItemAsync(oldItem, newItemData);
                newCalculation.CalculationResultItems.Add(newItem);
            }
            newCalculation.CreatorId = request.CreatorId;

            try
            {
                await uow.Calculations.AddAsync(newCalculation);
                await uow.SaveAsync();
                return new CalculationResponse(newCalculation);
            }
            catch (Exception ex)
            {
                return new CalculationResponse($"При обновлении расчета произошла непредвиденная ошибка: {ex.Message}");
            }
        }

        private async Task<Calculation> CreateCalculationAsync(IEnumerable<CalculationSource> calculationSources,
                                                               CreateCalculationRequest request,
                                                               User user,
                                                               Clinic clinic)
        {
            var calculateResultRequest = new CalculateResultRequest()
            {
                CalculationSources = calculationSources,
                MandatoryOrderExaminations = await orderService.GetMandatoryOrderExaminationsWithActualServicesAsync(clinic.Id)
            };
            return new Calculation()
            {
                CalculationResultItems = profCalculator.CalculateResult(calculateResultRequest).ToList(),
                CalculationSources = calculationSources.ToList(),
                Clinic = clinic,
                Creator = user,
                CreationDate = DateTime.Now,
                Name = request.Name,
            };
        }

        private async Task<Calculation> FindCalculationAsync(int id)
        {
            return await uow.Calculations.FindCalculationAsync(id);
        }

        private async Task<User> FindUserAsync(int id)
        {
            return await uow.Users.FindAsync(id);
        }

        private Calculation ModifyCalculation(Calculation old)
        {
            return new Calculation()
            {
                CalculationSources = old.CalculationSources,
                ClinicId = old.ClinicId,
                CreationDate = DateTime.Now,
                IsModified = true,
                Name = old.Name
            };
        }

        private async Task<CalculationResultItem> UpdateResultItemAsync(CalculationResultItem oldItem, UpdateCalculationResultItemRequest newItemData)
        {
            var request = new SaveServiceRequest()
            {
                ClinicId = oldItem.Service.ClinicId,
                OrderExaminationId = oldItem.Service.OrderExaminationId,
                Price = newItemData.Price,
                ServiceAvailabilityGroupId = newItemData.GroupId,
                ServiceDetails = oldItem.Service.ServiceDetails
            };

            return new CalculationResultItem()
            {
                Amount = newItemData.Amount,
                Service = await catalogService.CreateService(request)
            };
        }

        #endregion Methods
    }
}