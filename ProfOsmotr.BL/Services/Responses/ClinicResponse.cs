using ProfOsmotr.DAL;

namespace ProfOsmotr.BL
{
    /// <summary>
    /// Базовый ответ сервиса медицинских организаций
    /// </summary>
    public class ClinicResponse : BaseResponse<Clinic>
    {
        public ClinicResponse(Clinic entity) : base(entity)
        {
        }

        public ClinicResponse(string errorMessage) : base(errorMessage)
        {
        }
    }
}