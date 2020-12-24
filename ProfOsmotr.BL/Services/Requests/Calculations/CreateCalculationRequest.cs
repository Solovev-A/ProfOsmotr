using System.Collections.Generic;

namespace ProfOsmotr.BL
{
    /// <summary>
    /// Представляет исходные данные для производства расчета медицинского осмотра
    /// </summary>
    public class CreateCalculationRequest
    {
        /// <summary>
        /// Идентификатор пользователя, запросившего создание расчета
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Идентификатор медицинской организации, проводящей медицинский осмотр
        /// </summary>
        public int ClinicId { get; set; }

        /// <summary>
        /// Название расчета
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Перечисление исходных данных, необходимых для создания профессий, в отношении которых
        /// планируется проведение медицинского осмотра
        /// </summary>
        public IEnumerable<CreateProfessionRequest> CreateProfessionRequests { get; set; }
    }
}