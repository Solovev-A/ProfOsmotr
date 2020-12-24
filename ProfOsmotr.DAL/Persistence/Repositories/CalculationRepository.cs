using Microsoft.EntityFrameworkCore;
using ProfOsmotr.DAL.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProfOsmotr.DAL
{
    public class CalculationRepository : EFRepository<Calculation>, ICalculationRepository
    {
        public CalculationRepository(ProfContext context) : base(context)
        {
        }

        public async Task<PaginatedResult<Calculation>> ListAsync(CalculationsPaginationQuery query)
        {
            var allCalculations = dbSet.Where(calc => calc.ClinicId == query.ClinicId);

            var count = await allCalculations.CountAsync();

            var list = await allCalculations
                .Include(calc => calc.Creator.UserProfile)
                .AsNoTracking()
                .OrderByDescending(calc => calc.Id)
                .Skip((query.Page - 1) * query.ItemsPerPage)
                .Take(query.ItemsPerPage)
                .ToListAsync();

            return new PaginatedResult<Calculation>()
            {
                CurrentPage = query.Page,
                Items = list,
                TotalCount = count,
                TotalPages = (int)Math.Ceiling(count / (double)query.ItemsPerPage)
            };
        }

        public async Task<Calculation> FindCalculationAsync(int id)
        {
            return await dbSet
                .Include(calc => calc.CalculationSources)
                    .ThenInclude(source => source.Profession.ProfessionOrderItems)
                        .ThenInclude(item => item.OrderItem)
                .Include(calc => calc.Creator.UserProfile)
                .Include(calc => calc.CalculationResultItems)
                    .ThenInclude(item => item.Service.ServiceDetails)
                .Include(calc => calc.CalculationResultItems)
                    .ThenInclude(item => item.Service.OrderExamination)
                .FirstOrDefaultAsync(calc => calc.Id == id && !calc.IsDeleted);
        }
    }
}