using System.ComponentModel;

namespace ProfOsmotr.DAL
{
    /// <summary>
    /// Представляет идентификаторы групп доступности медицинской услуги
    /// </summary>
    public enum ServiceAvailabilityGroupId
    {
        [Description("Доступна")]
        Available = 1,

        [Description("Недоступна")]
        Unavailable,

        [Description("Включена")]
        Included
    }
}