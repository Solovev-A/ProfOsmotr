namespace ProfOsmotr.BL
{
    /// <summary>
    /// Представляет исходные данные для обновления подробного описания медицинской организации
    /// </summary>
    public class UpdateClinicDetailsRequest
    {
        /// <summary>
        /// Идентификатор обновляемой МО
        /// </summary>
        public int ClinicId { get; set; }

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