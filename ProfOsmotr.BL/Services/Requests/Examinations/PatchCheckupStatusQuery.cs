using ProfOsmotr.DAL;
using System;
using System.Collections.Generic;

namespace ProfOsmotr.BL
{
    public class PatchCheckupStatusQuery : PatchDtoBase
    {
        public int? EmployerDepartmentId { get; set; }

        public int? ProfessionId { get; set; }

        public IEnumerable<UpdateCheckupIndexValueRequest> CheckupIndexValues { get; set; }

        public CheckupResultId? CheckupResultId { get; set; }

        public string MedicalReport { get; set; }

        public DateTime? DateOfCompletion { get; set; }

        public int RegistrationJournalEntryNumber { get; set; }
    }
}