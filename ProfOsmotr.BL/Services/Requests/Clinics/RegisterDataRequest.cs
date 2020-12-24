namespace ProfOsmotr.BL
{
    /// <summary>
    /// Представляет данные для создания запроса на регистрацию медицинской организации
    /// </summary>
    public class RegisterDataRequest
    {
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

        /// <summary>
        /// Сведения о пользователе, подавшем запрос
        /// </summary>
        public CreateUserRequest User { get; set; }
    }
}