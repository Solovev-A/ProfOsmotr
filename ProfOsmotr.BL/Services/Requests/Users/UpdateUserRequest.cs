namespace ProfOsmotr.BL
{
    /// <summary>
    /// Представляет исходные данные для обновления пользователя
    /// </summary>
    public class UpdateUserRequest
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
        /// Идентификатор роли пользователя, либо null, если изменение роли не требуется
        /// </summary>
        public int? RoleId { get; set; }
    }
}