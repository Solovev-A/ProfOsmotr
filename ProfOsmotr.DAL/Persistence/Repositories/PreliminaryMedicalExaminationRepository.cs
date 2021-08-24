using Microsoft.EntityFrameworkCore;
using ProfOsmotr.DAL.Abstractions;
using ProfOsmotr.DAL.Infrastructure;
using System;
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

        public async Task<PreliminaryMedicalExamination> FindExaminationAsync(int id)
        {
            return await GetInitialQuery()
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