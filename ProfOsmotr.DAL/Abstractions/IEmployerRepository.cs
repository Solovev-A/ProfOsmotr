using System.Threading.Tasks;

namespace ProfOsmotr.DAL.Abstractions
{
    /// <summary>
    /// Представляет абстракцию хранилища работодателей
    /// </summary>
    public interface IEmployerRepository : IQueryAwareRepository<Employer>
    {
        /// <summary>
        /// Производит поиск работодателя с идентификатором <paramref name="id"/>
        /// </summary>
        /// <param name="id">Идентифиактор искомого работодателя</param>
        /// <returns>null, если поиск не дал результатов</returns>
        Task<Employer> FindEmployerAsync(int id);

        /// <summary>
        /// Производит поиск подразделения работодателя с идентификатором <paramref name="id"/>
        /// </summary>
        /// <param name="id">Идентификатор искомого подразделения работодателя</param>
        /// <param name="noTracking"></param>
        /// <returns>null, если поиск не дал результатов</returns>
        Task<EmployerDepartment> FindEmployerDepartmentAsync(int id, bool noTracking = true);
    }
}