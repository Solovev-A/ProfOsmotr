using ProfOsmotr.DAL;
using System.Collections.Generic;

namespace ProfOsmotr.BL
{
    public class CalculateResultRequest
    {
        public IEnumerable<CalculationSource> CalculationSources { get; set; }

        public IEnumerable<OrderExamination> MandatoryOrderExaminations { get; set; }
    }
}