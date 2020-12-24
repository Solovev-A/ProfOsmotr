using ProfOsmotr.DAL;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProfOsmotr.BL.Abstractions
{
    /// <summary>
    /// Представляет абстракцию для сервиса пользователей
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Добавляет нового пользователя
        /// </summary>
        /// <param name="clinicId">Идентификатор МО пользователя, либо null, если у пользователя нет МО</param>
        /// <param name="request">Исходные данные для создания пользователя</param>
        /// <returns></returns>
        Task<UserResponse> AddUserAsync(int? clinicId, CreateUserRequest request);

        /// <summary>
        /// Предоставляет список всех пользователей, соответствующих запросу
        /// </summary>
        /// <returns></returns>
        Task<QueryResponse<User>> ListAllUsers<TKey>(int start,
                                                     int length,
                                                     string search,
                                                     Expression<Func<User, TKey>> orderingSelector,
                                                     bool descending);

        /// <summary>
        /// Предоставляет список пользователей медицинской организации с идентификатором <paramref
        /// name="clinicId"/>, соответствующих запросу
        /// </summary>
        /// <returns></returns>
        Task<QueryResponse<User>> ListClinicUsers<TKey>(int clinicId,
                                                        int start,
                                                        int length,
                                                        string search,
                                                        Expression<Func<User, TKey>> orderingSelector,
                                                        bool descending);

        /// <summary>
        /// Обновляет пользователя с идентификатором <paramref name="id"/>
        /// </summary>
        /// <param name="id">Идентификатор обновляемого пользователя</param>
        /// <param name="request">Исходные данные для обновления пользователя</param>
        /// <returns></returns>
        Task<UserResponse> UpdateUserAsync(int id, UpdateUserRequest request);

        /// <summary>
        /// Проверяет валидность пароля <paramref name="password"/> пользователя с псевдонимом <paramref name="username"/>
        /// </summary>
        /// <param name="username">Псевдоним пользователя</param>
        /// <param name="password">Пароль для проверки</param>
        /// <returns></returns>
        Task<UserResponse> ValidatePassword(string username, string password);

        /// <summary>
        /// Предоставляет пользователя с идентификатором <paramref name="userId"/>
        /// </summary>
        /// <param name="userId">Идентификатор существующего пользователя</param>
        /// <returns></returns>
        Task<UserResponse> GetUser(int userId);
    }
}