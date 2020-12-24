namespace ProfOsmotr.DAL
{
    /// <summary>
    /// Представляет подробную информацию о медицинской организации
    /// </summary>
    public class ClinicDetails
    {
        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Идентификатор медицинской организации
        /// </summary>
        public int ClinicId { get; set; }

        /// <summary>
        /// Медицинская организация
        /// </summary>
        public virtual Clinic Clinic { get; set; }

        /// <summary>
        /// Полное наименование МО
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Сокращенное наименование МО
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// Адрес МО
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Телефон МО
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Электронная почта МО
        /// </summary>
        public string Email { get; set; }
    }
}