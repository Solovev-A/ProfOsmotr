using Microsoft.EntityFrameworkCore;
using ProfOsmotr.DAL.Abstractions;
using ProfOsmotr.DAL.Infrastructure;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProfOsmotr.DAL
{
    public class EmployerRepository : QueryAwareRepository<Employer>, IEmployerRepository
    {
        public EmployerRepository(ProfContext context) : base(context)
        {
        }

        public async Task<Employer> FindEmployerAsync(int id)
        {
            return await dbSet.AsNoTracking()
                .Include(e => e.Departments)
                .Include(e => e.PeriodicMedicalExaminations)
                .FirstOrDefaultAsync(employer => employer.Id == id);
        }

        public async Task<EmployerDepartment> FindEmployerDepartmentAsync(int id, bool noTracking = true)
        {
            var set = context.Set<EmployerDepartment>();
            var query = noTracking ? set.AsNoTracking() : set;
            return await query.FirstOrDefaultAsync(d => d.Id == id);
        }

        protected override IQueryable<Employer> GetInitialQuery()
        {
            return dbSet.AsNoTracking();
        }

        protected override Expression<Func<Employer, bool>> GetSearchFilterCondition(string search)
        {
            return x => EF.Functions.Like(x.Name, $"%{search}%")
                || EF.Functions.Like(x.Name, $"%{DBHelper.NormalizeSQLiteNameSearchQuery(search)}%");
        }
    }
}