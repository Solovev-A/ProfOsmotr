using ProfOsmotr.DAL.Abstractions;
using ProfOsmotr.Hashing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfOsmotr.DAL
{
    public class DataSeeder
    {
        public async Task Seed(IProfUnitOfWork uow, IPasswordHasher passwordHasher, OrderDataConfiguration configuration)
        {
            IEnumerable<OrderExamination> examinations = await new Order302Seeder(uow).Seed();
            User administrator = await new UserSeeder(uow, passwordHasher).SeedAsync();
            Clinic clinic = await new VirtualClinicSeeder(uow).SeedAsync();
            administrator.Clinic = clinic;
            await new ClinicCatalogSeeder().SeedDefaultCatalog(uow, clinic, examinations);

            await new ICD10Seeder(uow).Seed();
            await new MedicalExaminationDataSeeder(uow).Seed();

            await uow.SaveAsync();
        }
    }
}