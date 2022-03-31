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
    public class PreliminaryMedicalExaminationRepository
        : QueryAwareRepository<PreliminaryMedicalExamination>,
          IPreliminaryMedicalExaminationRepository
    {
        public PreliminaryMedicalExaminationRepository(ProfContext context) : base(context)
        {
        }

        public async Task<IEnumerable<CountResult>> CountExaminationsByMonth(int clinicId)
        {
            return await dbSet.AsNoTracking()
                .Where(ex => ex.ClinicId == clinicId && ex.CheckupStatus.DateOfCompletion.HasValue)
                .GroupBy(ex => new
                {
                    Month = ex.CheckupStatus.DateOfCompletion.Value.Month,
                    Year = ex.CheckupStatus.DateOfCompletion.Value.Year
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

        public async Task<PreliminaryMedicalExamination> FindExaminationAsync(int id, bool noTracking = true)
        {
            var query = noTracking ? dbSet.AsNoTracking() : dbSet;

            return await query
                .Include(ex => ex.Clinic.ClinicDetails)
                .Include(ex => ex.Employer)
                .Include(ex => ex.CheckupStatus.Patient)
                .Include(ex => ex.CheckupStatus.Profession.OrderItems)
                .Include(ex => ex.CheckupStatus.EmployerDepartment)
                .Include(ex => ex.LastEditor.UserProfile)
                .Include(ex => ex.CheckupStatus.CheckupResult)
                .Include(ex => ex.CheckupStatus.IndividualCheckupIndexValues)
                    .ThenInclude(indexValue => indexValue.ExaminationResultIndex.OrderExamination)
                .FirstOrDefaultAsync(ex => ex.Id == id);
        }

        protected override IQueryable<PreliminaryMedicalExamination> GetInitialQuery()
        {
            return dbSet.AsNoTracking()
                .Include(ex => ex.Employer)
                .Include(ex => ex.CheckupStatus.Patient)
                .Include(ex => ex.CheckupStatus.Profession.OrderItems);
        }

        protected override Expression<Func<PreliminaryMedicalExamination, bool>> GetSearchFilterCondition(string search)
        {
            var nameParts = search.Split(' ');
            string lastName = nameParts[0].Trim();
            string firstName = nameParts.Length > 1 ? nameParts[1].Trim() : string.Empty;
            string patronimycName = nameParts.Length > 2 ? nameParts[2].Trim() : string.Empty;

            return x => (EF.Functions.Like(x.CheckupStatus.Patient.LastName, $"{lastName}%")
            && EF.Functions.Like(x.CheckupStatus.Patient.FirstName, $"{firstName}%")
            && EF.Functions.Like(x.CheckupStatus.Patient.PatronymicName, $"{patronimycName}%"))
            || (EF.Functions.Like(x.CheckupStatus.Patient.LastName, $"{DBHelper.NormalizeSQLiteNameSearchQuery(lastName)}%")
            && EF.Functions.Like(x.CheckupStatus.Patient.FirstName, $"{DBHelper.NormalizeSQLiteNameSearchQuery(firstName)}%")
            && EF.Functions.Like(x.CheckupStatus.Patient.PatronymicName, $"{DBHelper.NormalizeSQLiteNameSearchQuery(patronimycName)}%"));
        }
    }
}