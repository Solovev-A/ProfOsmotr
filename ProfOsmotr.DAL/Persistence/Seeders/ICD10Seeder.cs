using ProfOsmotr.DAL.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProfOsmotr.DAL
{
    internal class ICD10Seeder
    {
        private IProfUnitOfWork uow;

        internal ICD10Seeder(IProfUnitOfWork uow)
        {
            this.uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        internal async Task Seed()
        {
            string icd10Json = await GetJsonAsync();
            var chapters = JsonSerializer.Deserialize<IEnumerable<ICD10Chapter>>(icd10Json);

            await uow.ICD10Chapters.AddRangeAsync(chapters);
        }

        private async Task<string> GetJsonAsync()
        {
            return await File.ReadAllTextAsync(OrderDataConfiguration.ICD10JsonPath);
        }
    }
}