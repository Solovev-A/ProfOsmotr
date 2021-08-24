using System.Collections.Generic;

namespace ProfOsmotr.Web.Models
{
    public class PreliminaryMedicalExaminationResource
    {
        public int Id { get; set; }

        public ExaminationPatientResource Patient { get; set; }

        public ExaminationEditorResource LastEditor { get; set; }

        public PreliminaryExaminationWorkPlaceResource WorkPlace { get; set; }

        public IEnumerable<CheckupExaminationResultIndexResource> CheckupExaminationResultIndexes { get; set; }

        public CheckupResultResource Result { get; set; }

        public string MedicalReport { get; set; }

        public string DateOfComplition { get; set; }

        public int? RegistrationJournalEntryNumber { get; set; }
    }
}