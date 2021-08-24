using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProfOsmotr.DAL
{
    public enum CheckupResultId
    {
        [Display(Name = "Годен")]
        [Description("Противопоказания к работе не выявлены")]
        NoContraindications,

        [Display(Name = "Не годен")]
        [Description("Выявлены постоянные противопоказания к работе")]
        PermanentContraindications,

        [Display(Name = "Временно не годен")]
        [Description("Выявлены временные противопоказания к работе")]
        TemporaryContraindications,

        [Display(Name = "Нуждается в дообследовании")]
        [Description("Нуждается в проведении дополнительного обследования (заключение не дано)")]
        NeedAdditionalMedicalExamination
    }
}