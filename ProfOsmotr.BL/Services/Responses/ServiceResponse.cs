using ProfOsmotr.DAL;

namespace ProfOsmotr.BL
{
    /// <summary>
    /// Представляет базовый ответ сервиса каталога в отношении медицинской услуги
    /// </summary>
    public class ServiceResponse : BaseResponse<Service>
    {
        public ServiceResponse(Service entity) : base(entity)
        {
        }

        public ServiceResponse(string errorMessage) : base(errorMessage)
        {
        }
    }
}