using ProfOsmotr.Web.Models;
using System.Threading.Tasks;

namespace ProfOsmotr.Web.Services
{
    /// <summary>
    /// Представляет абстракцию для сервиса аутентификации
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Производит аутентификацию пользователя
        /// </summary>
        /// <param name="resource">Исходные данные для аутентификации пользователя</param>
        /// <returns></returns>
        Task<AuthenticationResult> Authenticate(UserLoginQuery resource);

        /// <summary>
        /// Завершает сеанс работы пользователя
        /// </summary>
        /// <returns></returns>
        Task Logout();
    }
}