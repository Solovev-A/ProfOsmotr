using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfOsmotr.DAL.Abstractions
{
    public interface IPatientRepository : IQueryAwareRepository<Patient>
    {
        Task<Patient> FindPatientAsync(int id);
    }
}