using ProfOsmotr.DAL;
using System.Threading.Tasks;

namespace ProfOsmotr.BL.Abstractions
{
    /// <summary>
    /// Представляет абстракцию для сервиса пациентов
    /// </summary>
    public interface IPatientService
    {
        /// <summary>
        /// Производит поиск пациента с идентификатором <paramref name="id"/>
        /// </summary>
        /// <param name="id">Идентификатор пациента</param>
        /// <returns></returns>
        Task<PatientResponse> FindPatientAsync(int id);

        /// <summary>
        /// Добавляет пациента
        /// </summary>
        /// <param name="request">Запрос для добавления пациента</param>
        /// <returns></returns>
        Task<PatientResponse> CreatePatient(CreatePatientRequest request);

        /// <summary>
        /// Удаляет пациента с идентификатором <paramref name="id"/>
        /// </summary>
        /// <param name="id">Идентификатор пациента для удаления</param>
        /// <returns></returns>
        Task<PatientResponse> DeletePatientAsync(int id);

        /// <summary>
        /// Производит поиск пациентов с предположениями
        /// </summary>
        /// <param name="request">Запрос для поиска пациентов с предположениями</param>
        /// <returns></returns>
        /// <remarks>Предполагаемыми считаются пациенты, ранее проходившие медосмотр для определенного работодателя</remarks>
        Task<PatientSearchResultResponse> FindPatientWithSuggestions(FindPatientWithSuggestionsRequest request);

        /// <summary>
        /// Предоставляет пациента с идентификатором <paramref name="id"/>
        /// </summary>
        /// <param name="id">Идентификатор пациента</param>
        /// <returns></returns>
        Task<PatientResponse> GetPatientAsync(int id);

        /// <summary>
        /// Предоставляет список актуальных пациентов для клиники с идентификатором <paramref name="clinicId"/>
        /// </summary>
        /// <param name="clinicId">Идентификатор медицинской организации</param>
        /// <returns></returns>
        Task<QueryResponse<Patient>> ListActualPatientsAsync(int clinicId);

        /// <summary>
        /// Предоставляет список пациентов
        /// </summary>
        /// <param name="request">Запрос для постраничного списка пациентов</param>
        /// <returns></returns>
        Task<QueryResponse<Patient>> ListPatientsAsync(ListPatientsRequest request);

        /// <summary>
        /// Обновляет пациента
        /// </summary>
        /// <param name="request">Запрос для обновления пациента</param>
        /// <returns></returns>
        Task<PatientResponse> UpdatePatientAsync(UpdatePatientRequest request);
    }
}