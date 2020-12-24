using ProfOsmotr.DAL.Abstractions;
using System;
using System.Threading.Tasks;

namespace ProfOsmotr.DAL
{
    internal class VirtualClinicSeeder
    {
        private readonly IProfUnitOfWork uow;

        internal VirtualClinicSeeder(IProfUnitOfWork uow)
        {
            this.uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        /// <summary>
        /// Добавляет виртуальную медицинскую организацию, предназначенную для тестирования и
        /// администрирования, в хранилище
        /// </summary>
        /// <returns>Объект добавленной медицинской организации</returns>
        internal async Task<Clinic> SeedAsync()
        {
            var clinic = new Clinic()
            {
                ClinicDetails = new ClinicDetails()
                {
                    Address = "г. Брянск, Московский проспект, 201",
                    Email = "email@email.ru",
                    FullName = "Виртуальная медицинская организация",
                    Phone = "+74832123456",
                    ShortName = "Виртуальная МО"
                }
            };

            await uow.Clinics.AddAsync(clinic);

            return clinic;
        }
    }
}