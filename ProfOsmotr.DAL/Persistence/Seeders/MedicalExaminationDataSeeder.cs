using ProfOsmotr.DAL.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProfOsmotr.DAL
{
    internal class MedicalExaminationDataSeeder
    {
        private IProfUnitOfWork uow;

        internal MedicalExaminationDataSeeder(IProfUnitOfWork uow)
        {
            this.uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        internal async Task Seed()
        {
            await SeedGenders();
            await SeedCheckupResults();
        }

        private async Task SeedCheckupResults()
        {
            var checkupResults = Enum.GetValues(typeof(CheckupResultId))
                .Cast<CheckupResultId>()
                .Select(e => new CheckupResult() { Id = e, Text = e.Description() });
            await uow.CheckupResults.AddRangeAsync(checkupResults);
        }

        private async Task SeedGenders()
        {
            var genders = Enum.GetValues(typeof(GenderId))
                .Cast<GenderId>()
                .Select(e => new Gender() { Id = e, Name = e.Description() });
            await uow.Genders.AddRangeAsync(genders);
        }
    }
}