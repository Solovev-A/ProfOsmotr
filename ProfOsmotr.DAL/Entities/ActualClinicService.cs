namespace ProfOsmotr.DAL
{
    /// <summary>
    /// Представляет актуальную услугу медицинской организации, соответсвующую обследованию по приказу
    /// </summary>
    public class ActualClinicService
    {
        /// <summary>
        /// Идентификатор услуги
        /// </summary>
        public int ServiceId { get; set; }

        /// <summary>
        /// Медицинская услуга
        /// </summary>
        public virtual Service Service { get; set; }

        /// <summary>
        /// Идентификатор медицинской организации
        /// </summary>
        public int ClinicId { get; set; }

        /// <summary>
        /// Медицинская организация
        /// </summary>
        public virtual Clinic Clinic { get; set; }

        /// <summary>
        /// Идентификатор обследования по приказу
        /// </summary>
        public int OrderExaminationId { get; set; }

        /// <summary>
        /// Обследование по приказу
        /// </summary>
        public virtual OrderExamination OrderExamination { get; set; }
    }
}