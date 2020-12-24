using ProfOsmotr.DAL;
using System.Collections.Generic;

namespace ProfOsmotr.BL
{
    /// <summary>
    /// Базовый ответ сервиса каталога медицинских услуг
    /// </summary>
    public class CatalogResponse : BaseResponse<IEnumerable<Service>>
    {
        public CatalogResponse(IEnumerable<Service> entity) : base(entity)
        {
        }

        public CatalogResponse(string errorMessage) : base(errorMessage)
        {
        }
    }
}