using System.Collections.Generic;

namespace ProfOsmotr.Web.Models
{
    public class CheckupStatusResource
    {
        public CheckupStatusPatientResource Patient { get; set; }

        public EditorResource LastEditor { get; set; }

        public CheckupStatusWorkPlaceResource WorkPlace { get; set; }

        public IEnumerable<CheckupExaminationResultIndexResource> CheckupExaminationResultIndexes { get; set; }

        public CheckupResultResource Result { get; set; }

        public string MedicalReport { get; set; }

        public string DateOfCompletion { get; set; }

        public int? RegistrationJournalEntryNumber { get; set; }
    }
}