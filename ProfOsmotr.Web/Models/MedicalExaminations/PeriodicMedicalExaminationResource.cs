using System.Collections.Generic;

namespace ProfOsmotr.Web.Models
{
    public class PeriodicMedicalExaminationResource
    {
        public int Id { get; set; }

        public int ExaminationYear { get; set; }

        public PeriodicExaminationEmployerResource Employer { get; set; }

        public EmployerDataResource EmployerData { get; set; }

        public IEnumerable<ContingentCheckupStatusListItemResource> CheckupStatuses { get; set; }

        public string ReportDate { get; set; }

        public string Recommendations { get; set; }

        public ExaminationEditorResource LastEditor { get; set; }
    }
}