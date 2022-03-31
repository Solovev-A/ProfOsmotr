using System.Collections.Generic;

namespace ProfOsmotr.DAL
{
    /// <summary>
    /// Представляет состояние медицинского осмотра индивидуального пациента
    /// </summary>
    public class IndividualCheckupStatus : CheckupStatus
    {
        public int PreliminaryMedicalExaminationId { get; set; }

        public virtual PreliminaryMedicalExamination PreliminaryMedicalExamination { get; set; }

        public virtual ICollection<IndividualCheckupIndexValue> IndividualCheckupIndexValues { get; }
            = new List<IndividualCheckupIndexValue>();
    }
}