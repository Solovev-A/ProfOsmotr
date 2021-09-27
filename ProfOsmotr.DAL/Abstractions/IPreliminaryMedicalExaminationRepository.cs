using ProfOsmotr.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfOsmotr.DAL.Abstractions
{
    public interface IPreliminaryMedicalExaminationRepository : IQueryAwareRepository<PreliminaryMedicalExamination>
    {
        Task<IEnumerable<CountResult>> CountExaminationsByMonth(int clinicId);
        Task<PreliminaryMedicalExamination> FindExaminationAsync(int id, bool noTracking = true);
    }
}