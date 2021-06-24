using ProfOsmotr.DAL;
using System.Threading.Tasks;

namespace ProfOsmotr.BL.Abstractions
{
    public interface IEmployerService
    {
        Task<EmployerResponse> CreateEmployerAsync(CreateEmployerRequest request);

        Task<EmployerResponse> GetEmployerAsync(int id);
        Task<QueryResponse<Employer>> ListActualEmployersAsync(int clinicId);
        Task<QueryResponse<Employer>> ListEmployersAsync(ExequteQueryBaseRequest request);

        Task<EmployerResponse> UpdateEmployerAsync(UpdateEmployerRequest request);
    }
}