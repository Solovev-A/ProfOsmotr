namespace ProfOsmotr.DAL
{
    /// <summary>
    /// Представляет роль пользователя
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Идентификатор роли
        /// </summary>
        public RoleId Id { get; set; }

        /// <summary>
        /// Название роли
        /// </summary>
        public string Name { get; set; }
    }
}