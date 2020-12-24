using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfOsmotr.DAL.Abstractions
{
    /// <summary>
    /// Представляет абстракцию хранилища каталога услуг
    /// </summary>
    public interface ICatalogRepository : IRepository<ActualClinicService>
    {
        /// <summary>
        /// Предоставляет актуальные услуги медицинской организации с идентификатором <paramref
        /// name="clinicId"/>, включая подробную информацию, родительское обследование по приказу и
        /// группу доступности услуг
        /// </summary>
        /// <param name="clinicId">Идентификатор медицинской организации</param>
        /// <returns></returns>
        Task<IEnumerable<Service>> GetCatalogAsync(int clinicId);
    }
}