using System.ComponentModel;

namespace ProfOsmotr.DAL
{
    /// <summary>
    /// Идентификаторы целевых групп медицинского обследования
    /// </summary>
    public enum TargetGroupId
    {
        [Description("Для всех")]
        ForAll = 1,

        [Description("Только для женщин")]
        ForWomenOnly,

        [Description("Только для женщин старше 40 лет")]
        ForWomenOver40Only,

        [Description("Только для лиц старше 40 лет")]
        ForPersonsOver40Only
    }
}