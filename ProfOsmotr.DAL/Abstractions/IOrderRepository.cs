using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfOsmotr.DAL.Abstractions
{
    /// <summary>
    /// Представляет абстракцию хранилища приказа, устанавливающего порядок проведения медицинских осмотров
    /// </summary>
    public interface IOrderRepository : IRepository<OrderItem>
    {
        /// <summary>
        /// Предоставляет пункты приказа с обследованиями, сгруппированные по приложениям приказа
        /// </summary>
        /// <param name="nocache">Признак необходимости получения результата не из кэша</param>
        /// <returns>Перечисление приложений приказа</returns>
        Task<IEnumerable<OrderItem>> GetOrderAsync(bool nocache = false);

        /// <summary>
        /// Предоставляет пункт приказа с идентификатором <paramref name="itemId"/>, включая
        /// обследования и актуальные услуги для них, соответствующие медицинской организации с
        /// идентификатором <paramref name="clinicId"/>
        /// </summary>
        /// <param name="itemId">Идентификатор пункта приказа</param>
        /// <param name="clinicId">Идентификатор медицинской организации, услуги котороый должны быть включены</param>
        /// <returns></returns>
        Task<OrderItem> FindItemWithActualServicesAsync(int itemId, int clinicId);
    }
}