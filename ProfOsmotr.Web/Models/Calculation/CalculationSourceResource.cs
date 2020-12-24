using System.Collections.Generic;

namespace ProfOsmotr.Web.Models
{
    public class CalculationSourceResource
    {
        public string Profession { get; set; }

        public int NumberOfPersons { get; set; }

        public int NumberOfPersonsOver40 { get; set; }

        public int NumberOfWomen { get; set; }

        public int NumberOfWomenOver40 { get; set; }

        public IEnumerable<string> OrderItems { get; set; }
    }
}