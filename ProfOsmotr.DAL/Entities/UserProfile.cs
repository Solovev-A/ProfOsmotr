namespace ProfOsmotr.DAL
{
    /// <summary>
    /// Представляет подробную информацию о личности пользователя
    /// </summary>
    public class UserProfile
    {
        /// <summary>
        /// Идентификтор профиля
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Должность пользователя
        /// </summary>
        public string Position { get; set; }
    }
}