using ProfOsmotr.DAL;
using System.Collections.Generic;

namespace ProfOsmotr.BL
{
    /// <summary>
    /// Базовый ответ сервиса исходных данных для расчета медицинского осмотра
    /// </summary>
    public class CalculationSourcesResponse : BaseResponse<IEnumerable<CalculationSource>>
    {
        public CalculationSourcesResponse(IEnumerable<CalculationSource> entity) : base(entity)
        {
        }

        public CalculationSourcesResponse(string errorMessage) : base(errorMessage)
        {
        }
    }
}
