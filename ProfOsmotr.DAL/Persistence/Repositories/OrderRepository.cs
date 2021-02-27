using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProfOsmotr.DAL.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfOsmotr.DAL
{
    public class OrderRepository : EFRepository<OrderItem>, IOrderRepository
    {
        private const string itemsCollectionKey = "ORDER_ITEMS";
        private readonly IMemoryCache cache;

        public OrderRepository(ProfContext context, IMemoryCache cache) : base(context)
        {
            this.cache = cache;
        }

        public async Task<OrderItem> FindItemWithActualServicesAsync(int itemId, int clinicId)
        {
            IQueryable<OrderItem> query = GetOrderItemWithActualServicesQuery(clinicId);

            return await query.FirstOrDefaultAsync(item => item.Id == itemId && !item.IsDeleted);
        }


        public async Task<IEnumerable<OrderItem>> GetOrderAsync(bool nocache)
        {
            if (!cache.TryGetValue(itemsCollectionKey, out IEnumerable<OrderItem> value) || nocache)
            {
                value = await dbSet
                    .AsNoTracking()
                    .Include(item => item.OrderExaminations.OrderBy(ex => ex.Name))
                    .Where(item => !item.IsDeleted)
                    .ToListAsync();

                value = value.OrderBy(item => item.Key, new OrderItemKeyComparer());

                cache.Set(itemsCollectionKey, value, TimeSpan.FromMinutes(5));
            }
            return value;
        }

        private IQueryable<OrderItem> GetOrderItemWithActualServicesQuery(int clinicId)
        {
            return dbSet
                .Include(item => item.OrderExaminations)
                    .ThenInclude(ex => ex.ActualClinicServices.Where(actual => actual.ClinicId == clinicId))
                        .ThenInclude(actual => actual.Service.ServiceDetails);
        }
    }
}