using ProfOsmotr.BL.Abstractions;
using ProfOsmotr.BL.Infrastructure;
using ProfOsmotr.DAL;
using ProfOsmotr.DAL.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfOsmotr.BL
{
    public class ProfessionService : IProfessionService
    {
        #region Fields

        private readonly IOrderService orderService;
        private readonly IProfUnitOfWork uow;

        #endregion Fields

        #region Constructors

        public ProfessionService(IProfUnitOfWork uow, IOrderService orderService)
        {
            this.uow = uow;
            this.orderService = orderService;
        }

        #endregion Constructors

        #region Methods

        public async Task<ProfessionResponse> CreateProfession(CreateProfessionRequest request)
        {            
            return await CreateProfessionResponse(request);
        }

        public async Task<ProfessionResponse> CreateProfessionForCalculation(CreateProfessionRequest request, int clinicId)
        {
            return await CreateProfessionResponse(request, clinicId);
        }

        public async Task<IEnumerable<Profession>> GetAllProfessionsAsync()
        {
            return await uow.Professions.GetAllAsync();
        }

        public async Task<IEnumerable<Profession>> ListFavoritesAsync()
        {
            var professions = await uow.Professions.GetAllAsync();
            return professions
                .GroupBy(value => value, new ProfessionByNameEqualityComparer())
                .Select(group => (group.Key, group.Count()))
                .OrderByDescending(i => i.Item2)
                .Take(150)
                .Select(i => i.Key);
        }

        private async Task<ProfessionResponse> CreateProfessionResponse(CreateProfessionRequest request, int? clinicId = null)
        {
            if (request is null)
                return new ProfessionResponse("Запрос не может быть пустым");

            var profession = new Profession() { Name = request.Name };
            IEnumerable<OrderItem> generalOrderItems = await orderService.GetGeneralOrderItemsAsync();

            var orderItemIdentifiersToAdd = request.OrderItemIdentifiers
                .Concat(generalOrderItems.Select(item => item.Id));

            foreach (var id in orderItemIdentifiersToAdd)
            {
                var itemResponse = clinicId.HasValue 
                    ? await orderService.FindItemWithActualServicesAsync(id, clinicId.Value)
                    : await orderService.FindItemAsync(id);

                if (!itemResponse.Succeed)
                    return new ProfessionResponse(itemResponse.Message);
                profession.ProfessionOrderItems.Add(new ProfessionOrderItem() { OrderItem = itemResponse.Result });
            }

            return new ProfessionResponse(profession);
        }

        #endregion Methods
    }
}