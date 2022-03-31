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
                .Include(x => x.ExaminationResultIndexes)
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

        public async Task<IEnumerable<OrderExamination>> GetMandatoryExaminationsWithActualServicesAsync(int clinicId)
        {
            return await dbSet
                .Include(ex => ex.ActualClinicServices.Where(actual => actual.ClinicId == clinicId))
                    .ThenInclude(actual => actual.Service.ServiceDetails)
                .Where(ex => ex.IsMandatory)
                .ToListAsync();
        }
    }
}
