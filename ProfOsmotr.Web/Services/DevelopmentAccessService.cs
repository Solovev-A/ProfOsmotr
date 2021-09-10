using ProfOsmotr.Web.Models;
using System.Threading.Tasks;

namespace ProfOsmotr.Web.Services
{
    /// <summary>
    /// Реализация сервиса авторизации для использования в процессе разработки
    /// </summary>
    public class DevelopmentAccessService : IAccessService
    {
        private const int DEVELOPMENT_CLINIC_ID = 1;
        private const int DEVELOPMENT_USER_ID = 1;

        public async Task<AccessResult> CanAccessCalculationAsync(int calculationId)
        {
            return new AccessResult();
        }

        public async Task<AccessResult> CanAccessEmployerAsync(int employerId)
        {
            return new AccessResult();
        }

        public async Task<AccessResult> CanAccessEmployerDepartmentAsync(int employerDepartmentId)
        {
            return new AccessResult();
        }

        public async Task<AccessResult> CanAccessPatientAsync(int patientId)
        {
            return new AccessResult();
        }

        public async Task<AccessResult> CanAccessPeriodicExaminationAsync(int id)
        {
            return new AccessResult();
        }

        public async Task<AccessResult> CanAccessPreliminaryExaminationAsync(int examinationId)
        {
            return new AccessResult();
        }

        public async Task<AccessResult> CanManageUserAsync(int userId)
        {
            return new AccessResult();
        }

        public bool TryGetUserClinicId(out int clinicId)
        {
            clinicId = DEVELOPMENT_CLINIC_ID;
            return true;
        }

        public bool TryGetUserId(out int userId)
        {
            userId = DEVELOPMENT_USER_ID;
            return true;
        }
    }
}