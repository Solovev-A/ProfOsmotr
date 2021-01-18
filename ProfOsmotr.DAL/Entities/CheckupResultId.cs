using System.ComponentModel;

namespace ProfOsmotr.DAL
{
    public enum CheckupResultId
    {
        [Description("Противопоказания к работе не выявлены")]
        NoContraindications,

        [Description("Выявлены постоянные противопоказания к работе")]
        PermanentContraindications,

        [Description("Выявлены временные противопоказания к работе")]
        TemporaryContraindications,

        [Description("Нуждается в проведении дополнительного обследования (заключение не дано)")]
        NeedAdditionalMedicalExamination
    }
}