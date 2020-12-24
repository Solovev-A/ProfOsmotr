using ProfOsmotr.DAL;
using System.Threading.Tasks;

namespace ProfOsmotr.BL.Abstractions
{
    /// <summary>
    /// Представляет абстракцию для сервиса каталога медицинских услуг
    /// </summary>
    public interface ICatalogService
    {
        /// <summary>
        /// Позволяет получить каталог услуг медицинской организации с идентификатором <paramref name="clinicId"/>
        /// </summary>
        /// <param name="clinicId">Идентификатор медицинской организации</param>
        /// <returns></returns>
        Task<CatalogResponse> GetCatalogAsync(int clinicId);

        /// <summary>
        /// Обновляет актуальную медицинскую услугу. При этом старая услуга не удаляется, а
        /// актуальной помечается вновь созданная услуга
        /// </summary>
        /// <param name="request">Исходные данные для сохранения услуги</param>
        /// <returns></returns>
        Task<ServiceResponse> UpdateActualAsync(SaveServiceRequest request);

        /// <summary>
        /// Создает каталог по умолчанию для новых медицинских организаций
        /// </summary>
        /// <param name="clinic">Объект новой медицинской организации</param>
        /// <returns></returns>
        internal Task CreateDefaultCatalog(Clinic clinic);

        /// <summary>
        /// Создает новую медицинскую услугу
        /// </summary>
        /// <param name="request">Исходные данные для сохранения услуги</param>
        /// <returns></returns>
        internal Task<Service> CreateService(SaveServiceRequest request);

        /// <summary>
        /// Добавляет в каталоги всех медицинских организаций новую актуальную медицинскую услугу,
        /// соответствующую вновь добавленному обследованию <paramref name="newExamination"/>
        /// </summary>
        /// <param name="newExamination">Новое обследование по приказу</param>
        /// <returns></returns>
        internal Task SeedAllCatalogs(OrderExamination newExamination);
    }
}