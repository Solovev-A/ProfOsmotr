using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ProfOsmotr.DAL
{
    public class ClinicRegisterRequestRepository : QueryAwareRepository<ClinicRegisterRequest>
    {
        public ClinicRegisterRequestRepository(ProfContext context) : base(context)
        {
        }

        protected override IQueryable<ClinicRegisterRequest> GetInitialQuery()
        {
            return dbSet
                .AsNoTracking()
                .Include(x => x.Sender.UserProfile);
        }

        protected override Expression<Func<ClinicRegisterRequest, bool>> GetSearchFilterCondition(string search)
        {
            return x => EF.Functions.Like(x.ShortName, $"%{search}%")
                     || EF.Functions.Like(x.Sender.UserProfile.Name, $"%{search}%");
        }
    }
}