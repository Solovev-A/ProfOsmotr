using System.ComponentModel;

namespace ProfOsmotr.DAL
{
    public enum MedicalExaminationTypeId
    {
        [Description("Предварительный медицинский осмотр")]
        Preliminary,

        [Description("Периодический медицинский осмотр")]
        Periodic
    }
}