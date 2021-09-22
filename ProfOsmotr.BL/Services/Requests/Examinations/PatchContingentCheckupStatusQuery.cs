using System.Collections.Generic;

namespace ProfOsmotr.BL
{
    public class PatchContingentCheckupStatusQuery : PatchCheckupStatusQuery
    {
        public bool CheckupStarted { get; set; }

        public bool IsDisabled { get; set; }

        public bool NeedExaminationAtOccupationalPathologyCenter { get; set; }

        public bool NeedOutpatientExamunationAndTreatment { get; set; }

        public bool NeedInpatientExamunationAndTreatment { get; set; }

        public bool NeedSpaTreatment { get; set; }

        public bool NeedDispensaryObservation { get; set; }

        public IEnumerable<UpdateNewlyDiagnosedDiseaseRequest> NewlyDiagnosedChronicSomaticDiseases { get; set; }

        public IEnumerable<UpdateNewlyDiagnosedDiseaseRequest> NewlyDiagnosedOccupationalDiseases { get; set; }
    }
}