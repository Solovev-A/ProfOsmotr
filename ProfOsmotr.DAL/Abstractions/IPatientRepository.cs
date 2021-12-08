using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfOsmotr.DAL.Abstractions
{
    /// <summary>
    /// Представляет абстракцию хранилища данных о пациентах
    /// </summary>
    public interface IPatientRepository : IQueryAwareRepository<Patient>
    {
        /// <summary>
        /// Производит поиск пациента с идентификатором <paramref name="id"/>
        /// </summary>
        /// <param name="id">Идентификатор искомого пациента</param>
        /// <returns>null, если поиск не дал результов</returns>
        Task<Patient> FindPatientAsync(int id);

        /// <summary>
        /// Предоставляет предположения для поиска по запросу <paramref name="search"/> 
        /// пациентов клиники <paramref name="clinicId"/>, которые раньше проходили медосмотр 
        /// для работодателя с идентификатором <paramref name="employerId"/>
        /// </summary>
        /// <param name="search">Поисковый запрос</param>
        /// <param name="clinicId">Идентификатор клиники, в которой обслуживается пациент</param>
        /// <param name="employerId">Идентификатор работодателя</param>
        /// <returns></returns>
        Task<IEnumerable<Patient>> GetSuggestedPatients(string search, int clinicId, int employerId);
    }
}