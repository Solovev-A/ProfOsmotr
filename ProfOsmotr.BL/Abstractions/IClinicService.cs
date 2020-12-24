using ProfOsmotr.DAL;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProfOsmotr.BL.Abstractions
{
    /// <summary>
    /// Представляет абстракцию сервиса медицинских организаций
    /// </summary>
    public interface IClinicService
    {
        /// <summary>
        /// Добавляет новую медицинскую организацию
        /// </summary>
        /// <param name="requestId">
        /// Идентификатор сохраненного запроса на регистрацию медицинской организации
        /// </param>
        /// <returns></returns>
        Task<ClinicResponse> AddClinic(int requestId);

        /// <summary>
        /// Добавляет новый запрос на регистрацию медицинской организации
        /// </summary>
        /// <param name="request">Данные для создания запроса на регистрацию МО</param>
        /// <returns></returns>
        Task<RegisterRequestResponse> AddRegisterRequest(RegisterDataRequest request);

        /// <summary>
        /// Перечисляет медицинские организации, в соответствии с параметрами запроса
        /// </summary>
        /// <returns></returns>
        Task<QueryResponse<Clinic>> ListClinics<TKey>(int start, int length, string search, Expression<Func<Clinic, TKey>> orderingSelector, bool descending);

        /// <summary>
        /// Перечисляет новые запросы на регистрацию медицинской организации, в соответствии с
        /// параметрами запроса
        /// </summary>
        /// <returns></returns>
        Task<QueryResponse<ClinicRegisterRequest>> ListNewRegisterRequests<TKey>(int start, int length, string search, Expression<Func<ClinicRegisterRequest, TKey>> orderingSelector, bool descending);

        /// <summary>
        /// Перечисляет ранее обработанные запросы на регистрацию медицинской организации, в
        /// соответствии с параметрами запроса
        /// </summary>
        /// <returns></returns>
        Task<QueryResponse<ClinicRegisterRequest>> ListProcessedRegisterRequests<TKey>(int start, int length, string search, Expression<Func<ClinicRegisterRequest, TKey>> orderingSelector, bool descending);

        /// <summary>
        /// Предоставляет медицинскую организацию с идентификатором <paramref name="clinicId"/>
        /// </summary>
        /// <param name="clinicId">Идентификатор МО</param>
        /// <returns></returns>
        Task<ClinicResponse> GetClinic(int clinicId);

        /// <summary>
        /// Управляет статусом блокировки медицинской организации
        /// </summary>
        /// <param name="request">Исходные данные для управления статусом МО</param>
        /// <returns></returns>
        Task<ClinicResponse> ManageClinic(ManageClinicRequest request);

        /// <summary>
        /// Отклоняет запрос на регистрацию медицинской организации с идентификатором <paramref name="requestId"/>
        /// </summary>
        /// <param name="requestId">Идентификатор запроса на регистрацию МО</param>
        /// <returns></returns>
        Task<RegisterRequestResponse> RejectRegisterRequest(int requestId);

        /// <summary>
        /// Обновляет подробное описание медицинской организации
        /// </summary>
        /// <param name="request">Исходные данные для обновления подробного описания МО</param>
        /// <returns></returns>
        Task<ClinicResponse> UpdateDetails(UpdateClinicDetailsRequest request);
    }
}