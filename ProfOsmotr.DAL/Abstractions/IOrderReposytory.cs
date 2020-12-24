using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfOsmotr.DAL.Abstractions
{
    /// <summary>
    /// Представляет абстракцию хранилища приказа, устанавливающего порядок проведения медицинских осмотров
    /// </summary>
    public interface IOrderReposytory : IRepository<OrderItem>
    {
        /// <summary>
        /// Предоставляет актуальные элементы приказа
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<OrderItem>> GetActualItems();

        /// <summary>
        /// Предоставляет пункты приказа с обследованиями, сгруппированные по приложениям приказа
        /// </summary>
        /// <returns>Перечисление приложений приказа</returns>
        Task<IEnumerable<OrderAnnex>> GetOrderAsync();

        /// <summary>
        /// Предоставляет обследования по приказу, включая подробную информацию об услугах по умолчанию
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<OrderExamination>> GetExaminationsWithDetailsAsync();

        /// <summary>
        /// Предоставляет пункт приказа с идентификатором <paramref name="itemId"/>, включая
        /// обследования и актуальные услуги для них, соответствующие медицинской организации с
        /// идентификатором <paramref name="clinicId"/>
        /// </summary>
        /// <param name="itemId">Идентификатор пункта приказа</param>
        /// <param name="clinicId">Идентификатор медицинской организации, услуги котороый должны быть включены</param>
        /// <returns></returns>
        Task<OrderItem> FindItemWithActualServicesAsync(int itemId, int clinicId);
        Task<IEnumerable<OrderExamination>> GetExaminationsAsync();
    }
}