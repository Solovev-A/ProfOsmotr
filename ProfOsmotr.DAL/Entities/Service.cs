using System;

namespace ProfOsmotr.DAL
{
    /// <summary>
    /// Представляет медицинскую услугу
    /// </summary>
    public class Service
    {
        /// <summary>
        /// Идентификатор услуги
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор обследования по приказу, которому соответствует услуга
        /// </summary>
        public int OrderExaminationId { get; set; }

        /// <summary>
        /// Обследование по приказу, которому соответствует услуга
        /// </summary>
        public virtual OrderExamination OrderExamination { get; set; }

        /// <summary>
        /// Идентификатор медицинской организации, оказывающей услугу
        /// </summary>
        public int ClinicId { get; set; }

        /// <summary>
        /// Медицинская организация, оказывающая услугу
        /// </summary>
        public virtual Clinic Clinic { get; set; }

        /// <summary>
        /// Идентификатор подробного описания услуги
        /// </summary>
        public int ServiceDetailsId { get; set; }

        /// <summary>
        /// Подробное описание услуги
        /// </summary>
        public virtual ServiceDetails ServiceDetails { get; set; }

        /// <summary>
        /// Стоимость услуги
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Идентификатор группы доступности медицинской услуги
        /// </summary>
        public ServiceAvailabilityGroupId ServiceAvailabilityGroupId { get; set; }

        /// <summary>
        /// Группа доступности медицинской услуги
        /// </summary>
        public virtual ServiceAvailabilityGroup ServiceAvailabilityGroup { get; set; }

        /// <summary>
        /// Последнее время изменения услуги
        /// </summary>
        public System.DateTime UpdateTime { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Service other
                && other.Id == Id
                && other.OrderExaminationId == OrderExaminationId
                && other.ClinicId == ClinicId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, OrderExaminationId, ClinicId);
        }
    }
}