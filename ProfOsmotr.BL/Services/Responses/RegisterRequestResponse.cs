using ProfOsmotr.DAL;

namespace ProfOsmotr.BL
{
    /// <summary>
    /// Ответ сервиса медицинских организаций в отношении запросов на регистрацию
    /// </summary>
    public class RegisterRequestResponse : BaseResponse<ClinicRegisterRequest>
    {
        public RegisterRequestResponse(ClinicRegisterRequest entity) : base(entity)
        {
        }

        public RegisterRequestResponse(string errorMessage) : base(errorMessage)
        {
        }
    }
}