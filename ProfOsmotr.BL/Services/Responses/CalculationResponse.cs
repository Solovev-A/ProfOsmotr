using ProfOsmotr.DAL;

namespace ProfOsmotr.BL
{
    /// <summary>
    /// Базовый ответ сервиса расчетов
    /// </summary>
    public class CalculationResponse : BaseResponse<Calculation>
    {
        public CalculationResponse(Calculation entity) : base(entity)
        {
        }

        public CalculationResponse(string errorMessage) : base(errorMessage)
        {
        }
    }
}