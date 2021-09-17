using System.Threading.Tasks;

namespace ProfOsmotr.DAL.Abstractions
{
    public interface IPeriodicMedicalExaminationRepository : IQueryAwareRepository<PeriodicMedicalExamination>
    {
        Task<PeriodicMedicalExamination> FindExaminationAsync(int id, bool noTracking = false);

        Task<ContingentCheckupStatus> FindCheckupStatus(int id, bool noTracking = false);

        Task<int> GetCheckupStatusClinicIdAsync(int checkupStatusId);
    }
}