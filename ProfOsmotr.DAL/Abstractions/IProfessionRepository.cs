using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfOsmotr.DAL.Abstractions
{
    public interface IProfessionRepository : IQueryAwareRepository<Profession>
    {
        Task<IEnumerable<Profession>> GetSuggestedProfessions(string search, int employerId);
    }
}