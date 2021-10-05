using System.Collections.Generic;

namespace ProfOsmotr.BL.Models.ReportData
{
    public class CheckupResultsData
    {
        public string ExaminationName { get; set; }

        public IEnumerable<ExaminationResultIndexData> Indexes { get; set; }
    }
}