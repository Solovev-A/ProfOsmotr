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

        /// <summary>
        /// Проверяет, может ли текущий пользователь получить доступ к данным о пациенте
        /// с идентификатором <paramref name="patientId"/>
        /// </summary>
        /// <param name="patientId">Идентификатор пациента, доступ к данным которого проверяется</param>
        /// <returns></returns>
        Task<AccessResult> CanAccessPatientAsync(int patientId);

        /// <summary>
        /// Проверяет, может ли текущий пользователь получить доступ к периодическому медосмотру
        /// с идентификатором <paramref name="id"/>
        /// </summary>
        /// <param name="id">Идентификатор периодического медосмотра, доступ к которому проверяется</param>
        /// <returns></returns>
        Task<AccessResult> CanAccessPeriodicExaminationAsync(int id);

        /// <summary>
        /// Проверяет, может ли текущий пользователь получить доступ к организации
        /// с идентификатором <paramref name="employerId"/>
        /// </summary>
        /// <param name="employerId">Идентификатор организации, доступ к которой проверяется</param>
        /// <returns></returns>
        Task<AccessResult> CanAccessEmployerAsync(int employerId);

        /// <summary>
        /// Проверяет, может ли текущий пользователь получить доступ к предварительному медосмотру
        /// с идентификатором <paramref name="examinationId"/>
        /// </summary>
        /// <param name="examinationId">Идентификатор предварительного медосмотра, доступ к которому проверяется</param>
        /// <returns></returns>
        Task<AccessResult> CanAccessPreliminaryExaminationAsync(int examinationId);

        /// <summary>
        /// Проверяет, может ли текущий пользователь получить доступ к подразделению
        /// с идентификатором <paramref name="employerDepartmentId"/>
        /// </summary>
        /// <param name="employerDepartmentId">Идентификатор подразделения организации, доступ к которому проверяется</param>
        /// <returns></returns>
        Task<AccessResult> CanAccessEmployerDepartmentAsync(int employerDepartmentId);

        /// <summary>
        /// Проверяет, может ли текущий пользователь получить доступ к периодическому медосмотру работника
        /// с идентификтором <paramref name="id"/>
        /// </summary>
        /// <param name="id">Идентификатор периодического медосмотра работника, доступ к которому проверяется</param>
        /// <returns></returns>
        Task<AccessResult> CanAccessContingentCheckupStatus(int id);
    }
}