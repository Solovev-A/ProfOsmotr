using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ProfOsmotr.DAL
{
    public class UserRepository : QueryAwareRepository<User>
    {
        public UserRepository(ProfContext context) : base(context)
        {
        }

        protected override IQueryable<User> GetInitialQuery()
        {
            return dbSet.AsNoTracking()
                .Include(u => u.Clinic.ClinicDetails)
                .Include(u => u.Role)
                .Include(u => u.UserProfile);
        }

        protected override Expression<Func<User, bool>> GetSearchFilterCondition(string search)
        {
            return x => EF.Functions.Like(x.UserProfile.Name, $"%{search}%")
                     || EF.Functions.Like(x.Role.Name, $"%{search}%")
                     || EF.Functions.Like(x.Clinic.ClinicDetails.ShortName, $"%{search}%");
        }
    }
}