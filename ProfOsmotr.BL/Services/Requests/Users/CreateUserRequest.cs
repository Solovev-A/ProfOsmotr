namespace ProfOsmotr.BL
{
    /// <summary>
    /// Представляет исходные данные для создания пользователя
    /// </summary>
    public class CreateUserRequest
    {
        /// <summary>
        /// Псевдоним пользователя
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Должность пользователя
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Идентификатор роли пользователя
        /// </summary>
        public int RoleId { get; set; } = (int)DAL.RoleId.Blocked;
        // значение по умолчанию используется при создании запроса на регистрацию
    }
}