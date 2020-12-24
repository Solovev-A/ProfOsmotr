using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ProfOsmotr.DAL
{
    public class ClinicRepository : QueryAwareRepository<Clinic>
    {
        public ClinicRepository(ProfContext context) : base(context)
        {
        }

        protected override IQueryable<Clinic> GetInitialQuery()
        {
            return dbSet.AsNoTracking()
                .Include(x => x.ClinicDetails);
        }

        protected override Expression<Func<Clinic, bool>> GetSearchFilterCondition(string search)
        {
            return x => EF.Functions.Like(x.ClinicDetails.ShortName, $"%{search}%")
         || EF.Functions.Like(x.ClinicDetails.Phone, $"%{search}%")
         || EF.Functions.Like(x.ClinicDetails.Email, $"%{search}%");
        }
    }
}