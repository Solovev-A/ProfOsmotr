using ProfOsmotr.BL.Abstractions;
using ProfOsmotr.DAL;
using ProfOsmotr.DAL.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProfOsmotr.BL
{
    public class CatalogService : ICatalogService
    {
        #region Fields

        private readonly IProfUnitOfWork uow;

        #endregion Fields

        #region Constructors

        public CatalogService(IProfUnitOfWork uow)
        {
            this.uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        #endregion Constructors

        #region Methods

        async Task ICatalogService.CreateDefaultCatalog(Clinic clinic)
        {
            await new ClinicCatalogSeeder().SeedDefaultCatalog(uow, clinic);
        }

        async Task<Service> ICatalogService.CreateService(SaveServiceRequest request)
        {
            return new Service()
            {
                Id = await GetNewServiceIdAsync(request.ClinicId, request.OrderExaminationId),
                ClinicId = request.ClinicId,
                OrderExaminationId = request.OrderExaminationId,
                Price = request.Price,
                ServiceAvailabilityGroupId = (ServiceAvailabilityGroupId)request.ServiceAvailabilityGroupId,
                ServiceDetails = request.ServiceDetails,
                UpdateTime = DateTime.Now
            };
        }

        public async Task<CatalogResponse> GetCatalogAsync(int clinicId)
        {
            var clinic = await uow.Clinics.FirstOrDefaultAsync(clinic => clinicId == clinic.Id && !clinic.IsBlocked);
            if (clinic == null)
            {
                return new CatalogResponse("Медицинская организация не найдена");
            }

            var catalog = await uow.ActualClinicServices.GetCatalogAsync(clinicId);
            return new CatalogResponse(catalog);
        }

        async Task ICatalogService.SeedAllCatalogs(OrderExamination newExamination)
        {
            var clinics = await uow.Clinics.GetAllAsync();

            new ClinicCatalogSeeder().SeedNewExamination(clinics, newExamination);
        }

        public async Task<ServiceResponse> UpdateActualAsync(SaveServiceRequest request)
        {
            if (request is null)
                return new ServiceResponse("Запрос не может быть пустым");
            if (!Enum.IsDefined(typeof(ServiceAvailabilityGroupId), request.ServiceAvailabilityGroupId))
                return new ServiceResponse("Неизвестная группа доступности");
            if (request.Price < 0)
                return new ServiceResponse("Цена не может быть отрицательной");
            var actualItem = await uow.ActualClinicServices
                .FirstOrDefaultAsync(item => item.OrderExaminationId == request.OrderExaminationId
                                          && item.ClinicId == request.ClinicId);
            if (actualItem == null)
                return new ServiceResponse("Актуальная услуга не найдена");

            actualItem.Service = await UpdateService(actualItem.Service, request);

            try
            {
                uow.ActualClinicServices.Update(actualItem);
                await uow.SaveAsync();
                return new ServiceResponse(actualItem.Service);
            }
            catch (Exception ex)
            {
                return new ServiceResponse($"При обновлении услуги возникла непредвиденная ошибка: {ex.Message}");
            }
        }

        private async Task<int> GetNewServiceIdAsync(int clinicId, int orderExaminationId)
        {
            var oldServices = await uow.Services
                .FindAsync(s => s.ClinicId == clinicId && s.OrderExaminationId == orderExaminationId);

            return oldServices
                .Max(service => service.Id)
                + 1;
        }

        private async Task<Service> UpdateService(Service old, SaveServiceRequest request)
        {
            var group = await uow.ServiceAvailabilityGroups
                .FirstOrDefaultAsync(g => (ServiceAvailabilityGroupId)request.ServiceAvailabilityGroupId == g.Id);

            var result = new Service()
            {
                Id = await GetNewServiceIdAsync(old.ClinicId, old.OrderExaminationId),
                Clinic = old.Clinic,
                OrderExamination = old.OrderExamination,
                Price = request.Price,
                ServiceAvailabilityGroup = group,
                UpdateTime = DateTime.Now
            };

            if (request.ServiceDetails.Code == old.ServiceDetails.Code && request.ServiceDetails.FullName == old.ServiceDetails.FullName)
                result.ServiceDetails = old.ServiceDetails;
            else
                result.ServiceDetails = request.ServiceDetails;

            return result;
        }

        #endregion Methods
    }
}