using Microsoft.EntityFrameworkCore;
using ProfOsmotr.DAL.Abstractions;
using ProfOsmotr.DAL.Infrastructure;
using ProfOsmotr.DAL.Models;
using System;
using System.Collections.Generic;
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

        public void DeleteCheckupStatus(ContingentCheckupStatus checkupStatus)
        {
            context.Set<ContingentCheckupStatus>().Remove(checkupStatus);
        }

        public async Task<IEnumerable<CountResult>> CountCheckupsByMonth(int clinicId)
        {
            return await context.Set<ContingentCheckupStatus>()
                .AsNoTracking()
                .Where(s => s.PeriodicMedicalExamination.ClinicId == clinicId && s.DateOfCompletion.HasValue)
                .GroupBy(s => new
                {
                    Month = s.DateOfCompletion.Value.Month,
                    Year = s.DateOfCompletion.Value.Year
                })
                .OrderBy(g => g.Key.Year)
                .ThenBy(g => g.Key.Month)
                .Select(g => new CountResult()
                {
                    Period = $"{g.Key.Year}-{g.Key.Month:D2}",
                    Count = g.Count()
                })
                .ToListAsync();
        }

        public async Task<ContingentCheckupStatus> FindCheckupStatus(int id, bool noTracking = false)
        {
            return await GetInitialContingentCheckupStatusQuery(noTracking)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<ContingentCheckupStatus>> ListAllCheckupStatuses(int examinationId)
        {
            return await GetInitialContingentCheckupStatusQuery(true)
                .Where(s => s.PeriodicMedicalExaminationId == examinationId)
                .OrderBy(s => s.Patient.LastName)
                .ThenBy(s => s.Patient.FirstName)
                .ThenBy(s => s.Patient.PatronymicName)
                .ToListAsync();
        }

        public async Task<PeriodicMedicalExamination> FindExaminationAsync(int id, bool noTracking = false)
        {
            var initial = GetInitialPeriodicMedicalExaminationQuery();
            var query = noTracking ? initial.AsNoTracking() : initial;

            return await query
                .Include(ex => ex.Statuses)
                    .ThenInclude(s => s.Patient)
                .Include(ex => ex.Statuses)
                    .ThenInclude(s => s.Profession.OrderItems.Where(oi => !oi.IsDeleted))
                .Include(ex => ex.LastEditor.UserProfile)
                .Include(ex => ex.Statuses
                                    .OrderBy(s => s.Patient.LastName)
                                    .ThenBy(s => s.Patient.FirstName)
                                    .ThenBy(s => s.Patient.PatronymicName))
                .FirstOrDefaultAsync(ex => ex.Id == id);
        }

        public async Task<PeriodicMedicalExamination> FindExaminationReportData(int id)
        {
            return await GetPeriodicExaminationReportDataQuery()
                .Include(ex => ex.Statuses)
                    .ThenInclude(s => s.Profession.OrderItems.Where(oi => !oi.IsDeleted))
                .Include(ex => ex.Statuses)
                    .ThenInclude(s => s.EmployerDepartment)
                .Include(ex => ex.Statuses
                                    .OrderBy(s => s.Patient.LastName)
                                    .ThenBy(s => s.Patient.FirstName)
                                    .ThenBy(s => s.Patient.PatronymicName))
                .FirstOrDefaultAsync(ex => ex.Id == id);
        }

        public async Task<IEnumerable<PeriodicMedicalExamination>> FindAllExaminations(int examinationYear, int clinicId)
        {
            return await GetPeriodicExaminationReportDataQuery()
                .Where(ex => ex.ExaminationYear == examinationYear && ex.ClinicId == clinicId)
                .ToListAsync();
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
            // для списка периодических медосмотров

            return dbSet.AsNoTracking()
                .Include(e => e.Employer);
        }

        protected override Expression<Func<PeriodicMedicalExamination, bool>> GetSearchFilterCondition(string search)
        {
            return x => EF.Functions.Like(x.Employer.Name, $"%{search}%")
                || EF.Functions.Like(x.Employer.Name, $"%{DBHelper.NormalizeSQLiteNameSearchQuery(search)}%");
        }

        protected IQueryable<ContingentCheckupStatus> GetInitialContingentCheckupStatusQuery(bool noTracking)
        {
            var set = context.Set<ContingentCheckupStatus>();
            var query = noTracking ? set.AsNoTracking() : set;

            return query
                .Include(s => s.CheckupResult)
                .Include(s => s.ContingentCheckupIndexValues)
                    .ThenInclude(i => i.ExaminationResultIndex.OrderExamination)
                .Include(s => s.EmployerDepartment)
                .Include(s => s.LastEditor.UserProfile)
                .Include(s => s.Patient)
                .Include(s => s.PeriodicMedicalExamination.Employer)
                .Include(s => s.PeriodicMedicalExamination.Clinic.ClinicDetails)
                .Include(s => s.Profession.OrderItems)
                .Include(s => s.NewlyDiagnosedChronicSomaticDiseases)
                    .ThenInclude(d => d.ICD10Chapter)
                .Include(s => s.NewlyDiagnosedOccupationalDiseases)
                    .ThenInclude(d => d.ICD10Chapter);
        }

        protected IQueryable<PeriodicMedicalExamination> GetInitialPeriodicMedicalExaminationQuery()
        {
            return dbSet
                .Include(ex => ex.Employer)
                .Include(ex => ex.EmployerData)
                .Include(ex => ex.Statuses)
                    .ThenInclude(s => s.CheckupResult);
        }

        protected IQueryable<PeriodicMedicalExamination> GetPeriodicExaminationReportDataQuery()
        {
            return GetInitialPeriodicMedicalExaminationQuery()
                .AsNoTracking()
                .Include(ex => ex.Statuses)
                    .ThenInclude(s => s.Patient)
                .Include(ex => ex.Statuses)
                    .ThenInclude(s => s.NewlyDiagnosedChronicSomaticDiseases)
                        .ThenInclude(d => d.ICD10Chapter)
                .Include(ex => ex.Statuses)
                    .ThenInclude(s => s.NewlyDiagnosedOccupationalDiseases)
                        .ThenInclude(d => d.ICD10Chapter)
                .Include(ex => ex.Clinic.ClinicDetails);
        }
    }
}