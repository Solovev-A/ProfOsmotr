using Microsoft.EntityFrameworkCore;
using ProfOsmotr.DAL.Abstractions;
using ProfOsmotr.DAL.Infrastructure;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace ProfOsmotr.DAL
{
    public class PeriodicMedicalExaminationRepository
        : QueryAwareRepository<PeriodicMedicalExamination>,
        IPeriodicMedicalExaminationRepository
    {
        public PeriodicMedicalExaminationRepository(ProfContext context) : base(context)
        {
        }

        protected override IQueryable<PeriodicMedicalExamination> GetInitialQuery()
        {
            return dbSet.AsNoTracking()
                .Include(e => e.Employer);
        }

        protected override Expression<Func<PeriodicMedicalExamination, bool>> GetSearchFilterCondition(string search)
        {
            return x => EF.Functions.Like(x.Employer.Name, $"%{search}%")
                || EF.Functions.Like(x.Employer.Name, $"%{DBHelper.NormalizeSQLiteNameSearchQuery(search)}%");
        }
    }
}