using System.Collections.Generic;

namespace ProfOsmotr.Web.Models
{
    public class ContingentCheckupStatusResource : CheckupStatusResource
    {
        public int Id { get; set; }

        public ContingentCheckupStatusExaminationResource Examination { get; set; }

        public bool CheckupStarted { get; set; }

        public bool IsDisabled { get; set; }

        public bool NeedExaminationAtOccupationalPathologyCenter { get; set; }

        public bool NeedOutpatientExamunationAndTreatment { get; set; }

        public bool NeedInpatientExamunationAndTreatment { get; set; }

        public bool NeedSpaTreatment { get; set; }

        public bool NeedDispensaryObservation { get; set; }

        public IEnumerable<NewlyDiagnosedDiseaseResource> NewlyDiagnosedChronicSomaticDiseases { get; set; }

        public IEnumerable<NewlyDiagnosedDiseaseResource> NewlyDiagnosedOccupationalDiseases { get; set; }
    }
}