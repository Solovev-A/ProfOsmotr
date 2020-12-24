using System.Collections.Generic;

namespace ProfOsmotr.BL
{
    /// <summary>
    /// Представляет исходные данные для обновления расчета
    /// </summary>
    public class UpdateCalculationRequest
    {
        /// <summary>
        /// Идентификатор обновляемого расчета
        /// </summary>
        public int CalculationId { get; set; }

        /// <summary>
        /// Идентификатор пользователя, обновляющего расчет
        /// </summary>
        public int CreatorId { get; set; }

        /// <summary>
        /// Перечисление исходных данных для обновления результатов расчета
        /// </summary>
        public IEnumerable<UpdateCalculationResultItemRequest> ResultItems { get; set; }
    }
}