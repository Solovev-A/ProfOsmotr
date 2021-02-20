using System;
using System.Collections.Generic;
using System.Text;
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

        Task<IEnumerable<OrderExamination>> GetExaminationsAsync();

        Task<IEnumerable<ExaminationResultIndex>> GetIndexes(int examinationId);
    }
}
