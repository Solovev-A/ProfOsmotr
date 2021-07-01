using Microsoft.AspNetCore.Http;
using ProfOsmotr.BL.Abstractions;
using ProfOsmotr.DAL;
using ProfOsmotr.Web.Infrastructure.Extensions;
using ProfOsmotr.Web.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProfOsmotr.Web.Services
{
    /// <summary>
    /// Сервис авторизации
    /// </summary>
    public class AccessService : IAccessService
    {
        #region Fields

        private readonly ICalculationService calculationService;
        private readonly IEmployerService employerService;
        private readonly IExaminationsService examinationsService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IPatientService patientService;
        private readonly IUserService userService;

        #endregion Fields

        #region Constructors

        public AccessService(
            ICalculationService calculationService,
            IEmployerService employerService,
            IExaminationsService examinationsService,
            IHttpContextAccessor httpContextAccessor,
            IPatientService patientService,
            IUserService userService)
        {
            this.calculationService = calculationService ?? throw new ArgumentNullException(nameof(calculationService));
            this.employerService = employerService ?? throw new ArgumentNullException(nameof(employerService));
            this.examinationsService = examinationsService ?? throw new ArgumentNullException(nameof(examinationsService));
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            this.patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        #endregion Constructors

        #region Properties

        private ClaimsPrincipal User => httpContextAccessor.HttpContext.User;

        #endregion Properties

        #region Methods

        public async Task<AccessResult> CanAccessCalculationAsync(int calculationId)
        {
            return await GetAccessResultBasedOnClinicId(calculationId, CanWorkWithCalculation);
        }

        public async Task<AccessResult> CanAccessEmployerAsync(int employerId)
        {
            return await GetAccessResultBasedOnClinicId(employerId, CanWorkWithEmployer);
        }

        public async Task<AccessResult> CanAccessPatientAsync(int patientId)
        {
            return await GetAccessResultBasedOnClinicId(patientId, CanWorkWithPatient);
        }

        public async Task<AccessResult> CanAccessPrealiminaryExaminationAsync(int examinationId)
        {
            return await GetAccessResultBasedOnClinicId(examinationId, CanWorkWithPreliminaryExamination);
        }

        public async Task<AccessResult> CanManageUserAsync(int userId)
        {
            if (User.IsInRole(RoleId.Administrator.ToClaimValue()))
            {
                return new AccessResult();
            }

            return await GetAccessResultBasedOnClinicId(userId, CanManageUser);
        }

        public bool TryGetUserClinicId(out int clinicId)
        {
            bool result = TryGetIntClaimValue(AuthenticationService.ClinicIdClaimType, out int value);
            clinicId = value;
            return result;
        }

        public bool TryGetUserId(out int userId)
        {
            bool result = TryGetIntClaimValue(AuthenticationService.UserIdClaimType, out int value);
            userId = value;
            return result;
        }

        private async Task<AccessResult> CanManageUser(int userId, int currentUserClinicId)
        {
            var userResponse = await userService.GetUser(userId);
            if (!userResponse.Succeed)
            {
                return new AccessDeniedResult(userResponse.Message);
            }

            if (userResponse.Result.ClinicId.HasValue && userResponse.Result.ClinicId.Value == currentUserClinicId)
            {
                return new AccessResult();
            }

            return new AccessDeniedResult();
        }

        private async Task<AccessResult> CanWorkWithCalculation(int calculationId, int currentUserClinicId)
        {
            var calculationResponse = await calculationService.GetCalculation(calculationId);
            if (!calculationResponse.Succeed)
            {
                return new AccessDeniedResult(calculationResponse.Message);
            }

            if (calculationResponse.Result.ClinicId == currentUserClinicId)
            {
                return new AccessResult();
            }

            return new AccessDeniedResult();
        }

        private async Task<AccessResult> CanWorkWithEmployer(int employerId, int currentUserClinicId)
        {
            var employerResponse = await employerService.GetEmployerAsync(employerId);
            if (!employerResponse.Succeed)
            {
                return new AccessDeniedResult(employerResponse.Message);
            }

            if (employerResponse.Result.ClinicId == currentUserClinicId)
            {
                return new AccessResult();
            }

            return new AccessDeniedResult();
        }

        private async Task<AccessResult> CanWorkWithPatient(int patientId, int currentUserClinicId)
        {
            var patientResponse = await patientService.GetPatientAsync(patientId);
            if (!patientResponse.Succeed)
            {
                return new AccessDeniedResult(patientResponse.Message);
            }

            if (patientResponse.Result.ClinicId == currentUserClinicId)
            {
                return new AccessResult();
            }

            return new AccessDeniedResult();
        }

        private async Task<AccessResult> CanWorkWithPreliminaryExamination(int examinationId, int currentUserClinicId)
        {
            var examinationClinicId = await examinationsService.GetPreliminaryMedicalExaminationClinicIdAsync(examinationId);
            if (examinationClinicId < 0)
            {
                return new AccessDeniedResult();
            }

            if (examinationClinicId == currentUserClinicId)
            {
                return new AccessResult();
            }

            return new AccessDeniedResult();
        }

        private async Task<AccessResult> GetAccessResultBasedOnClinicId(int entityId, Func<int, int, Task<AccessResult>> accessCheck)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return new AccessDeniedResult("Пользователь не аутентифицирован");
            }

            if (TryGetUserClinicId(out int currentUserClinicId))
            {
                return await accessCheck(entityId, currentUserClinicId);
            }

            return new AccessDeniedResult("Не удалось идентифицировать медицинскую организацию пользователя");
        }

        private bool TryGetIntClaimValue(string claimType, out int claimValue)
        {
            string value = User.FindFirstValue(claimType);
            if (int.TryParse(value, out int result))
            {
                claimValue = result;
                return true;
            }
            claimValue = default;
            return false;
        }

        #endregion Methods
    }
}