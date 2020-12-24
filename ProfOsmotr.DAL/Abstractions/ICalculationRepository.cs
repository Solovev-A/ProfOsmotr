using System.Threading.Tasks;

namespace ProfOsmotr.DAL.Abstractions
{
    /// <summary>
    /// Представляет абстракцию для хранилища расчетов медицинского осмотра
    /// </summary>
    public interface ICalculationRepository : IRepository<Calculation>
    {
        /// <summary>
        /// Предоставляет расчет с идентификатором <paramref name="id"/>, включая данные о
        /// пользователе, создавшем расчет, результатах расчета, подробной информации об услугах,
        /// включенных в расчет
        /// </summary>
        /// <param name="id">Идентификатор расчета</param>
        /// <returns>Искомый расчет, либо null, если он не найден</returns>
        Task<Calculation> FindCalculationAsync(int id);

        /// <summary>
        /// Предоставляет часть расчетов, удовлетворяющих запросу для постраничной выдачи <paramref name="query"/>
        /// </summary>
        /// <param name="query">Запрос для постраничной выдачи расчетов</param>
        /// <returns></returns>
        Task<PaginatedResult<Calculation>> ListAsync(CalculationsPaginationQuery query);
    }
}