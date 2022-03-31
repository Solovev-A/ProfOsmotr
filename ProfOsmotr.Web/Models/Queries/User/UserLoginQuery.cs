namespace ProfOsmotr.Web.Models
{
    /// <summary>
    /// Представляет исходные данные для аутентификации пользователя
    /// </summary>
    public class UserLoginQuery
    {
        /// <summary>
        /// Псевдоним пользователя
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password { get; set; }
    }
}