using Microsoft.EntityFrameworkCore;
using ProfOsmotr.DAL.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfOsmotr.DAL
{
    public class OrderExaminationRepository : EFRepository<OrderExamination>, IOrderExaminationRepository
    {
        public OrderExaminationRepository(ProfContext context) : base(context)
        {
        }

        public async Task<IEnumerable<OrderExamination>> GetExaminationsAsync()
        {
            return await dbSet
                .AsNoTracking()
                .OrderBy(examination => examination.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrderExamination>> GetExaminationsWithDetailsAsync()
        {
            return await dbSet
                .AsNoTracking()
                .Include(x => x.DefaultServiceDetails)
                .OrderBy(examination => examination.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<ExaminationResultIndex>> GetIndexes(int examinationId)
        {
            return await context.ExaminationResultIndexes
                .AsNoTracking()
                .Where(index => index.OrderExaminationId == examinationId && !index.IsDeleted)
                .OrderBy(index => index.Title)
                .ToListAsync();
        }
    }
}
