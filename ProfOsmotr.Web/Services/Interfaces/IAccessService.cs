using ProfOsmotr.Web.Models;
using System.Threading.Tasks;

namespace ProfOsmotr.Web.Services
{
    /// <summary>
    /// Представляет абстракцию для сервиса авторизации
    /// </summary>
    public interface IAccessService
    {
        /// <summary>
        /// Проверяет, может ли текущий пользователь получить доступ к расчету с идентификатором
        /// <paramref name="calculationId"/>
        /// </summary>
        /// <param name="calculationId">Идентификатор расчета, к которому запрашивается доступ</param>
        /// <returns></returns>
        Task<AccessResult> CanAccessCalculationAsync(int calculationId);

        /// <summary>
        /// Предоставляет идентификатор медицинской организации текущего пользователя
        /// </summary>
        /// <param name="clinicId">out параметр для результата</param>
        /// <returns>true, если идентификатор получен, иначе - false</returns>
        bool TryGetUserClinicId(out int clinicId);

        /// <summary>
        /// Предоставляет идентификатор текущего пользователя
        /// </summary>
        /// <param name="userId">out параметр для результата</param>
        /// <returns>true, если идентификатор получен, иначе - false</returns>
        bool TryGetUserId(out int userId);

        /// <summary>
        /// Проверяет, может ли текущий пользователь получить доступ к управлению пользователем с
        /// идентификатором <paramref name="userId"/>
        /// </summary>
        /// <param name="userId">Идентификатор пользователя, доступ к управлению котором проверяется</param>
        /// <returns></returns>
        Task<AccessResult> CanManageUserAsync(int userId);

        Task<AccessResult> CanAccessPatientAsync(int patientId);

        Task<AccessResult> CanAccessEmployerAsync(int employerId);
        Task<AccessResult> CanAccessPrealiminaryExaminationAsync(int examinationId);
    }
}