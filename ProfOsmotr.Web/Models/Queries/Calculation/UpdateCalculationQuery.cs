using System.Collections.Generic;

namespace ProfOsmotr.Web.Models
{
    public class UpdateCalculationQuery
    {
        public int CalculationId { get; set; }

        public IEnumerable<UpdateCalculationResultItemQuery> ResultItems { get; set; }
    }
}