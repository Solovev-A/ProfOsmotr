using ProfOsmotr.DAL;
using System.Threading.Tasks;

namespace ProfOsmotr.BL.Abstractions
{
    /// <summary>
    /// Представляет абстракцию для управления работодателями
    /// </summary>
    public interface IEmployerService
    {
        /// <summary>
        /// Добавляет нового работодателя
        /// </summary>
        /// <param name="request">Запрос с данными о добавляемого работодателя</param>
        /// <returns></returns>
        Task<EmployerResponse> CreateEmployerAsync(CreateEmployerRequest request);

        /// <summary>
        /// Добавляет новое подразделение работодателя
        /// </summary>
        /// <param name="request">Запрос с данными для добавления подразделения работодателя</param>
        /// <returns></returns>
        Task<EmployerDepartmentResponse> CreateEmployerDepartmentAsync(CreateEmployerDepartmentRequest request);

        /// <summary>
        /// Производит поиск подразделения работодателя с идентификатором <paramref name="id"/>
        /// </summary>
        /// <param name="id">Идентификатор подразделения работодателя</param>
        /// <param name="noTracking"></param>
        /// <returns></returns>
        Task<EmployerDepartmentResponse> FindEmployerDepartmentAsync(int id, bool noTracking = true);

        /// <summary>
        /// Предоставляет работодателя с идентификатором <paramref name="id"/>
        /// </summary>
        /// <param name="id">Идентификатор работодателя</param>
        /// <returns></returns>
        Task<EmployerResponse> GetEmployerAsync(int id);

        /// <summary>
        /// Предоставляет список актуальных на данный момент работодателей для медицинской организации с идентификатором <paramref name="clinicId"/>
        /// </summary>
        /// <param name="clinicId">Идентифиактор медицинской организации</param>
        /// <returns></returns>
        Task<QueryResponse<Employer>> ListActualEmployersAsync(int clinicId);

        /// <summary>
        /// Предоставляет список работодателей, удовлетворяющих запросу <paramref name="request"/>
        /// </summary>
        /// <param name="request">Запрос для работодателей</param>
        /// <returns></returns>
        Task<QueryResponse<Employer>> ListEmployersAsync(ExecuteQueryBaseRequest request);

        /// <summary>
        /// Обновляет работодателя
        /// </summary>
        /// <param name="request">Запрос с данными для обновления работодателя</param>
        /// <returns></returns>
        Task<EmployerResponse> UpdateEmployerAsync(UpdateEmployerRequest request);

        /// <summary>
        /// Обновляет подразделение работодателя
        /// </summary>
        /// <param name="request">Запрос с данными для обновления подразделения работодателя</param>
        /// <returns></returns>
        Task<EmployerDepartmentResponse> UpdateEmployerDepartmentAsync(UpdateEmployerDepartmentRequest request);
    }
}