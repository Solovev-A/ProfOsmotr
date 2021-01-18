using System.Collections.Generic;

namespace ProfOsmotr.DAL
{
    public class ContingentMedicalExamination : MedicalExamination
    {
        public virtual ICollection<ContingentCheckupStatus> Statuses { get; } = new List<ContingentCheckupStatus>();
    }
}