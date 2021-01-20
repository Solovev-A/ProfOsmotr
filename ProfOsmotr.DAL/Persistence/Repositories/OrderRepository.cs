using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ProfOsmotr.DAL.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfOsmotr.DAL
{
    public class OrderRepository : EFRepository<OrderItem>, IOrderReposytory
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

        public async Task<IEnumerable<OrderItem>> GetActualItems()
        {
            if (!cache.TryGetValue(itemsCollectionKey, out IEnumerable<OrderItem> value))
            {
                value = await dbSet
                    .Where(item => !item.IsDeleted)
                    .ToListAsync();
                value = value.OrderBy(item => item.Key, new OrderItemKeyComparer()).ToList();
                
                cache.Set(itemsCollectionKey, value, TimeSpan.FromMinutes(5));
            }
            return value;
        }

        public async Task<IEnumerable<OrderExamination>> GetExaminationsAsync()
        {
            return await context.OrderExaminations
                .AsNoTracking()
                .OrderBy(examination => examination.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrderExamination>> GetExaminationsWithDetailsAsync()
        {
            return await context.OrderExaminations
                .AsNoTracking()
                .Include(x => x.DefaultServiceDetails)
                .OrderBy(examination => examination.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrderAnnex>> GetOrderAsync()
        {
            var annexes = await context.OrderAnnex
                .AsNoTracking()
                .Include(x => x.OrderItems.Where(item => !item.IsDeleted))
                    .ThenInclude(x => x.OrderExaminations.OrderBy(ex => ex.Name))
                .ToListAsync();

            return annexes.Select(annex =>
            {
                annex.OrderItems = annex.OrderItems.OrderBy(item => item.Key, new OrderItemKeyComparer()).ToList();
                return annex;
            });
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