using ProfOsmotr.DAL;
using System.Collections.Generic;

namespace ProfOsmotr.BL.Abstractions
{
    /// <summary>
    /// Представляет абстракцию для калькулятора состава и количества медцинских услуг,
    /// необходимых для проведения медицинского осмотра
    /// </summary>
    public interface IProfCalculator
    {
        /// <summary>
        /// Рассчитывает состав и количество медицинских услуг, необходимых для проведения
        /// медицинского осмотра
        /// </summary>
        /// <param name="sources">Первичные исходные данные для расчета медицинского осмотра</param>
        /// <returns>Результаты расчета медицинского осмотра</returns>
        IEnumerable<CalculationResultItem> CalculateResult(CalculateResultRequest request);
    }
}