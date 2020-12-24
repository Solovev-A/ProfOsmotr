using System.Threading.Tasks;

namespace ProfOsmotr.BL.Abstractions
{
    /// <summary>
    /// Представляет абстракцию для создания исходных данных расчета медицинского осмотра
    /// </summary>
    public interface ICalculationSourceFactory
    {
        /// <summary>
        /// Создает исходные данные для расчета медицинского осмотра
        /// </summary>
        /// <param name="request">Исходные данные для производства расчета</param>
        /// <returns></returns>
        Task<CalculationSourcesResponse> CreateCalculationSources(CreateCalculationRequest request);
    }
}