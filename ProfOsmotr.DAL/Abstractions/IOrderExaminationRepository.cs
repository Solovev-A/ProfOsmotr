using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfOsmotr.DAL.Abstractions
{
    public interface IOrderExaminationRepository : IRepository<OrderExamination>
    {
        /// <summary>
        /// Предоставляет обследования по приказу, включая подробную информацию об услугах по умолчанию
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<OrderExamination>> GetExaminationsWithDetailsAsync();

        /// <summary>
        /// Предоставляет обследования по приказу
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<OrderExamination>> GetExaminationsAsync();

        /// <summary>
        /// Предоставляет показатели результата обследования с идентификатором <paramref name="examinationId"/>
        /// </summary>
        /// <param name="examinationId">Идентификатор обследования по приказу, для которого
        /// предоставляются показатели результата</param>
        /// <returns></returns>
        Task<IEnumerable<ExaminationResultIndex>> GetIndexes(int examinationId);

        /// <summary>
        /// Предоставляет обязательные обследования по приказу, включая актуальные услуги, соответствующие этим обследованиям
        /// для клиники с идентификатором <paramref name="clinicId"/>
        /// </summary>
        /// <param name="clinicId"></param>
        /// <returns></returns>
        Task<IEnumerable<OrderExamination>> GetMandatoryExaminationsWithActualServicesAsync(int clinicId);
    }
}