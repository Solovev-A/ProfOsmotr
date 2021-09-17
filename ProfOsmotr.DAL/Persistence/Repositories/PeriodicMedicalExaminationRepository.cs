using Microsoft.EntityFrameworkCore;
using ProfOsmotr.DAL.Abstractions;
using ProfOsmotr.DAL.Infrastructure;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProfOsmotr.DAL
{
    public class PeriodicMedicalExaminationRepository
        : QueryAwareRepository<PeriodicMedicalExamination>,
        IPeriodicMedicalExaminationRepository
    {
        public PeriodicMedicalExaminationRepository(ProfContext context) : base(context)
        {
        }

        public async Task<ContingentCheckupStatus> FindCheckupStatus(int id, bool noTracking = false)
        {
            var set = context.Set<ContingentCheckupStatus>();
            var query = noTracking ? set.AsNoTracking() : set;

            return await query
                .Include(s => s.CheckupResult)
                .Include(s => s.ContingentCheckupIndexValues)
                    .ThenInclude(i => i.ExaminationResultIndex.OrderExamination)
                .Include(s => s.EmployerDepartment)
                .Include(s => s.LastEditor.UserProfile)
                .Include(s => s.Patient)
                .Include(s => s.PeriodicMedicalExamination.Employer)
                .Include(s => s.Profession.OrderItems)
                .Include(s => s.NewlyDiagnosedChronicSomaticDiseases)
                .Include(s => s.NewlyDiagnosedOccupationalDiseases)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<PeriodicMedicalExamination> FindExaminationAsync(int id, bool noTracking = false)
        {
            var query = noTracking ? dbSet.AsNoTracking() : dbSet;

            return await query
                .Include(ex => ex.Employer)
                .Include(ex => ex.EmployerData)
                .Include(ex => ex.Statuses)
                    .ThenInclude(s => s.Patient)
                .Include(ex => ex.Statuses)
                    .ThenInclude(s => s.Profession.OrderItems.Where(oi => !oi.IsDeleted))
                .Include(ex => ex.Statuses)
                    .ThenInclude(s => s.CheckupResult)
                .Include(ex => ex.LastEditor.UserProfile)
                .FirstOrDefaultAsync(ex => ex.Id == id);
        }

        public async Task<int> GetCheckupStatusClinicIdAsync(int checkupStatusId)
        {
            var checkupStatus = await context
                .Set<ContingentCheckupStatus>()
                .Include(s => s.PeriodicMedicalExamination)
                .FirstOrDefaultAsync(s => s.Id == checkupStatusId);

            return checkupStatus?.PeriodicMedicalExamination.ClinicId ?? -1;
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