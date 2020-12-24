using ProfOsmotr.DAL;

namespace ProfOsmotr.BL
{
    /// <summary>
    /// Представляет исходные данные для сохранения медицинской услуги
    /// </summary>
    public class SaveServiceRequest
    {
        /// <summary>
        /// Идентификатор медицинской организации
        /// </summary>
        public int ClinicId { get; set; }

        /// <summary>
        /// Идентификатор обследования по приказу
        /// </summary>
        public int OrderExaminationId { get; set; }

        /// <summary>
        /// Стоимость услуги
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Идентификатор группы доступности услуги
        /// </summary>
        public int ServiceAvailabilityGroupId { get; set; }

        /// <summary>
        /// Подробное описание услуги
        /// </summary>
        public ServiceDetails ServiceDetails { get; set; }
    }
}