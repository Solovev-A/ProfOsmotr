using System;

namespace ProfOsmotr.BL
{
    public class PatchPeriodicExaminationQuery : PatchDtoBase
    {
        public PatchEmployerDataQuery EmployerData { get; set; }

        public string Recommendations { get; set; }

        public DateTime? ReportDate { get; set; }
    }
}