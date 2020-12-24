using System.Collections.Generic;

namespace ProfOsmotr.Web.Models
{
    public class UpdateCalculationResource
    {
        public int CalculationId { get; set; }

        public IEnumerable<UpdateCalculationResultItemResource> ResultItems { get; set; }
    }
}