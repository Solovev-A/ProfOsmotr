using System.ComponentModel;

namespace ProfOsmotr.DAL
{
    /// <summary>
    /// Идентификатор роли пользователя
    /// </summary>
    public enum RoleId
    {
        [Description("Администратор сайта")]
        Administrator = 1,

        [Description("Модератор клиники")]
        ClinicModerator,

        [Description("Сотрудник")]
        Employee,

        [Description("Заблокированный")]
        Blocked
    }
}