using System;
using System.Collections.Generic;

namespace ProfOsmotr.DAL
{
    /// <summary>
    /// Представляет состояние медицинского осмотра пациента
    /// </summary>
    public abstract class CheckupStatus
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public virtual Patient Patient { get; set; }

        public int? EmployerDepartmentId { get; set; }

        public virtual EmployerDepartment EmployerDepartment { get; set; }

        public int? ProfessionId { get; set; }

        public virtual Profession Profession { get; set; }

        public DateTime? DateOfCompletion { get; set; }

        public CheckupResultId? CheckupResultId { get; set; }

        public virtual CheckupResult CheckupResult { get; set; }

        public string MedicalReport { get; set; }

        public int? RegistrationJournalEntryNumber { get; set; }

        public int LastEditorId { get; set; }

        public virtual User LastEditor { get; set; }
    }
}