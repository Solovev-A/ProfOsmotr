using System.Collections.Generic;
using System.Linq;

namespace ProfOsmotr.Web.Models
{
    public class CalculationResource : CalculationGeneralDataResource
    {
        public IEnumerable<CalculationSourceResource> Sources { get; set; }

        public IEnumerable<CalculationResultItemResource> Results { get; set; }

        public int NumberOfPersons => Sources.Sum(source => source.NumberOfPersons);

        public int NumberOfPersonsOver40 => Sources.Sum(source => source.NumberOfPersonsOver40);

        public int NumberOfWomen => Sources.Sum(source => source.NumberOfWomen);

        public int NumberOfWomenOver40 => Sources.Sum(source => source.NumberOfWomenOver40);

        public decimal TotalSum => Results.Where(result => result.ServiceAvailabilityGroupId == 1)
                                          .Sum(item => item.Sum);

        public bool IsForCompany => NumberOfPersons > 1;
    }
}