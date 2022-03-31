using System;
using System.Collections.Generic;

namespace ProfOsmotr.DAL
{
    public class PeriodicMedicalExamination : MedicalExamination
    {
        public int? EmployerDataId { get; set; }

        public virtual EmployerData EmployerData { get; set; }

        public int ExaminationYear { get; set; }

        public DateTime? ReportDate { get; set; }

        public virtual ICollection<ContingentCheckupStatus> Statuses { get; } = new List<ContingentCheckupStatus>();
    }
}