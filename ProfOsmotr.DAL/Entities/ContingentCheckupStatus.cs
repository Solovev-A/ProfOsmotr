using System.Collections.Generic;

namespace ProfOsmotr.DAL
{
    /// <summary>
    /// Представляет состояние медицинского осмотра пациента, как части контингента, подлежащего осмотру
    /// </summary>
    public class ContingentCheckupStatus : CheckupStatus
    {
        public int PeriodicMedicalExaminationId { get; set; }

        public virtual PeriodicMedicalExamination PeriodicMedicalExamination { get; set; }

        public bool CheckupStarted { get; set; }

        /// <summary>
        /// Установлена ли пациенту стойкая степень утраты трудоспособности на момент осмотра
        /// </summary>
        public bool IsDisabled { get; set; }

        public bool NeedExaminationAtOccupationalPathologyCenter { get; set; }

        public bool NeedOutpatientExamunationAndTreatment { get; set; }

        public bool NeedInpatientExamunationAndTreatment { get; set; }

        public bool NeedSpaTreatment { get; set; }

        public bool NeedDispensaryObservation { get; set; }

        public virtual ICollection<NewlyDiagnosedChronicSomaticDisease> NewlyDiagnosedChronicSomaticDiseases { get; set; }
            = new List<NewlyDiagnosedChronicSomaticDisease>();

        public virtual ICollection<NewlyDiagnosedOccupationalDisease> NewlyDiagnosedOccupationalDiseases { get; set; }
            = new List<NewlyDiagnosedOccupationalDisease>();

        public virtual ICollection<ContingentCheckupIndexValue> ContingentCheckupIndexValues { get; }
            = new List<ContingentCheckupIndexValue>();
    }
}