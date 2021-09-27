using ProfOsmotr.DAL;
using ProfOsmotr.DAL.Models;
using System.Threading.Tasks;

namespace ProfOsmotr.BL.Abstractions
{
    /// <summary>
    /// Представляет абстракцию для сервиса расчетов состава, количества и стоимости медицинских
    /// услуг, необходимых для проведения медицинского осмотра
    /// </summary>
    public interface ICalculationService
    {
        /// <summary>
        /// Производит расчет медицинского осмотра
        /// </summary>
        /// <param name="request">Исходные данные для производства расчета</param>
        /// <returns></returns>
        Task<CalculationResponse> MakeCalculation(CreateCalculationRequest request);

        /// <summary>
        /// Производит обновление существующего расчета медицинского осмотра. При этом исходный
        /// расчет не изменяется, и создается новый расчет, содержащий внесенные изменения
        /// </summary>
        /// <param name="request">Исходные данные для обновления расчета</param>
        /// <returns></returns>
        Task<CalculationResponse> UpdateCalculationAsync(UpdateCalculationRequest request);

        /// <summary>
        /// Предоставляет доступ к существующему расчету
        /// </summary>
        /// <param name="id">Идентификатор сохраненного расчета</param>
        /// <returns></returns>
        Task<CalculationResponse> GetCalculation(int id);

        /// <summary>
        /// Обеспечивает постраничную выдачу списка произведенных расчетов
        /// </summary>
        /// <param name="request">Запрос для постраничной выдачи списка расчетов</param>
        /// <returns>Страница списка расчетов, соответствующая параметрам</returns>
        Task<PaginatedResult<Calculation>> ListAsync(CalculationsPaginationQuery request);
    }
}