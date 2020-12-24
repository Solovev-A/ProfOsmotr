using ProfOsmotr.DAL.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfOsmotr.DAL
{
    public class ClinicCatalogSeeder
    {
        /// <summary>
        /// Добавляет в хранилища <paramref name="uow"/> каталог по умолчанию для медицинской
        /// организации <paramref name="clinic"/>
        /// </summary>
        /// <param name="uow">Сервис работы с хранилищами</param>
        /// <param name="clinic">Новая медицинская организация, для которой добавляется каталог</param>
        /// <param name="examinationsToSeed">
        /// Обследования, услуги для которых будут добавлены в каталог. Если null, то список
        /// обследований будет получен из хранилища обследований
        /// </param>
        public async Task SeedDefaultCatalog(IProfUnitOfWork uow,
                                             Clinic clinic,
                                             IEnumerable<OrderExamination> examinationsToSeed = null)
        {
            if (clinic.Services.Any())
                throw new InvalidOperationException("Каталог по умолчанию можно создать только для новой клиники");

            var orderExaminations = examinationsToSeed ?? await uow.OrderExaminations.GetAllAsync();
            foreach (var examination in orderExaminations)
            {
                var service = CreateFirstService(clinic, examination);
                var actualItem = new ActualClinicService()
                {
                    Clinic = clinic,
                    OrderExamination = examination,
                    Service = service
                };
                await uow.ActualClinicServices.AddAsync(actualItem);
            }
        }

        public void SeedNewExamination(IEnumerable<Clinic> clinics, OrderExamination newExamination)
        {
            foreach (var clinic in clinics)
            {
                clinic.ActualClinicServices.Add(new ActualClinicService()
                {
                    OrderExamination = newExamination,
                    Service = CreateFirstService(clinic, newExamination)
                });
            }
        }

        private Service CreateFirstService(Clinic clinic, OrderExamination examination)
        {
            return new Service()
            {
                Id = 0,
                Clinic = clinic,
                OrderExamination = examination,
                Price = 0,
                ServiceDetails = CreateServiceDetails(examination),
                ServiceAvailabilityGroupId = ServiceAvailabilityGroupId.Available,
                UpdateTime = new DateTime(2020, 1, 1)
            };
        }

        private ServiceDetails CreateServiceDetails(OrderExamination examination)
        {
            return new ServiceDetails()
            {
                Code = examination.DefaultServiceDetails.Code,
                FullName = examination.DefaultServiceDetails.FullName
            };
        }
    }
}