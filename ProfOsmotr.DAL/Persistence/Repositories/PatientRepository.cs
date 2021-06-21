using Microsoft.EntityFrameworkCore;
using ProfOsmotr.DAL.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProfOsmotr.DAL
{
    public class PatientRepository : QueryAwareRepository<Patient>, IPatientRepository
    {
        public PatientRepository(ProfContext context) : base(context)
        {
        }

        public async Task<Patient> FindPatientAsync(int id)
        {
            return await dbSet.AsNoTracking()
                .Include(patient => patient.ContingentCheckupStatuses)
                    .ThenInclude(cs => cs.PeriodicMedicalExamination.Employer)
                .Include(patient => patient.ContingentCheckupStatuses)
                    .ThenInclude(cs => cs.Profession.OrderItems)
                .Include(patient => patient.IndividualCheckupStatuses)
                    .ThenInclude(cs => cs.PreliminaryMedicalExamination.Employer)
                .Include(patient => patient.IndividualCheckupStatuses)
                    .ThenInclude(cs => cs.Profession.OrderItems)
                .FirstOrDefaultAsync(patient => patient.Id == id);
        }

        protected override IQueryable<Patient> GetInitialQuery()
        {
            return dbSet.AsNoTracking();
        }

        protected override Expression<Func<Patient, bool>> GetSearchFilterCondition(string search)
        {
            var nameParts = search.Split(' ');
            string lastName = nameParts[0].Trim();
            string firstName = nameParts.Length > 1 ? nameParts[1].Trim() : string.Empty;
            string patronimycName = nameParts.Length > 2 ? nameParts[2].Trim() : string.Empty;

            return x => (EF.Functions.Like(x.LastName, $"{lastName}%")
            && EF.Functions.Like(x.FirstName, $"{firstName}%")
            && EF.Functions.Like(x.PatronymicName, $"{patronimycName}%"))
            || (EF.Functions.Like(x.LastName, $"{NormalizeSQLiteNameSearchQuery(lastName)}%")
            && EF.Functions.Like(x.FirstName, $"{NormalizeSQLiteNameSearchQuery(firstName)}%")
            && EF.Functions.Like(x.PatronymicName, $"{NormalizeSQLiteNameSearchQuery(patronimycName)}%"));
        }

        private string NormalizeSQLiteNameSearchQuery(string query)
        {
            // SQLite не поддерживает регистронезависмый фильтр для кириллицы (без низкоуровневых танцев с бубном)
            // Эта нормализация не решает проблему, но повышает качество поиска

            if (string.IsNullOrEmpty(query))
                return string.Empty;

            string lower = query.ToLower();
            return lower.First().ToString().ToUpper() + lower.Substring(1);
        }
    }
}