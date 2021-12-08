using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfOsmotr.DAL.Abstractions
{
    /// <summary>
    /// Представляет абстракцию для хранилища профессий
    /// </summary>
    public interface IProfessionRepository : IQueryAwareRepository<Profession>
    {
        /// <summary>
        /// Предоставляет предположения для поиска по запросу <paramref name="search"/> по профессиям, 
        /// которые ранее уже фигурировали в медосмотрах работодателя с идентификатором <paramref name="employerId"/>
        /// </summary>
        /// <param name="search">Поисковый запрос</param>
        /// <param name="employerId">Идентификатор работодателя</param>
        /// <returns></returns>
        Task<IEnumerable<Profession>> GetSuggestedProfessions(string search, int employerId);
    }
}