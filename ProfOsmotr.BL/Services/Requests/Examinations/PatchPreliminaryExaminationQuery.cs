using ProfOsmotr.DAL;
using System;
using System.Collections.Generic;

namespace ProfOsmotr.BL
{
    public class PatchPreliminaryExaminationQuery : PatchDtoBase
    {
        public int? EmployerId { get; set; }

        public int? EmployerDepartmentId { get; set; }

        public int? ProfessionId { get; set; }

        public IEnumerable<UpdateCheckupIndexValueRequest> CheckupIndexValues { get; set; }

        public CheckupResultId? CheckupResultId { get; set; }

        public string MedicalReport { get; set; }

        public DateTime DateOfComplition { get; set; }

        public int RegistrationJournalEntryNumber { get; set; }
    }
}