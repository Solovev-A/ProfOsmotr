using System.Threading.Tasks;

namespace ProfOsmotr.DAL.Abstractions
{
    public interface IPreliminaryMedicalExaminationRepository : IQueryAwareRepository<PreliminaryMedicalExamination>
    {
        Task<PreliminaryMedicalExamination> FindExaminationAsync(int id, bool noTracking = true);
    }
}