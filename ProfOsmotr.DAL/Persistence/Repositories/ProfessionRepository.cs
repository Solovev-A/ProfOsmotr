using Microsoft.EntityFrameworkCore;
using ProfOsmotr.DAL.Abstractions;
using ProfOsmotr.DAL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProfOsmotr.DAL
{
    public class ProfessionRepository : QueryAwareRepository<Profession>, IProfessionRepository
    {
        public ProfessionRepository(ProfContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Profession>> GetSuggestedProfessions(string search, int employerId)
        {
            var query = GetInitialQuery()
                .Where(GetSearchFilterCondition(search));

            var professionsAtIndividualCheckupStatuses = await query
                .Where(prof => prof.IndividualCheckupStatuses
                                    .Any(ch => ch.PreliminaryMedicalExamination.EmployerId == employerId))
                .ToListAsync();

            var professionsAtContingentCheckupStatuses = await query
                .Where(prof => prof.ContingentCheckupStatuses
                                    .Any(ch => ch.PeriodicMedicalExamination.EmployerId == employerId))
                .ToListAsync();

            return professionsAtContingentCheckupStatuses
                .Concat(professionsAtIndividualCheckupStatuses)
                .Distinct(new ProfessionByIdEqualityComparer());
        }

        protected override IQueryable<Profession> GetInitialQuery()
        {
            return dbSet.AsNoTracking()
                .Include(profession => profession.OrderItems.Where(item => !item.IsDeleted));
        }

        protected override Expression<Func<Profession, bool>> GetSearchFilterCondition(string search)
        {
            return x => EF.Functions.Like(x.Name, $"%{search}%")
                || EF.Functions.Like(x.Name, $"%{DBHelper.NormalizeSQLiteNameSearchQuery(search)}%");
        }
    }
}