using Microsoft.EntityFrameworkCore;
using ProfOsmotr.DAL.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfOsmotr.DAL
{
    public class CatalogRepository : EFRepository<ActualClinicService>, ICatalogRepository
    {
        public CatalogRepository(ProfContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Service>> GetCatalogAsync(int clinicId)
        {
            return await dbSet
                .AsNoTracking()
                .Include(actual => actual.Service.ServiceAvailabilityGroup)
                .Include(actual => actual.Service.ServiceDetails)
                .Include(actual => actual.Service.OrderExamination)
                .Where(actual => actual.ClinicId == clinicId)
                .Select(actual => actual.Service)
                .ToListAsync();
        }
    }
}