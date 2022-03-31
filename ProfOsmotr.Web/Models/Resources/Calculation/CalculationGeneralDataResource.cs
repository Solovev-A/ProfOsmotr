using System;

namespace ProfOsmotr.Web.Models
{
    public class CalculationGeneralDataResource
    {
        public int Id { get; set; }

        public bool IsModified { get; set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public string CreatorName { get; set; }

        public string CreatorPosition { get; set; }
    }
}