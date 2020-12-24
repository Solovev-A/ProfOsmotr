using Microsoft.EntityFrameworkCore;
using ProfOsmotr.DAL.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProfOsmotr.DAL
{
    public class EFRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        #region Fields

        protected ProfContext context;
        protected DbSet<TEntity> dbSet;

        #endregion Fields

        #region Constructors

        public EFRepository(ProfContext context)
        {
            this.context = context;
            dbSet = this.context.Set<TEntity>();
        }

        #endregion Constructors

        #region Methods

        public virtual async Task AddAsync(TEntity item)
        {
            await dbSet.AddAsync(item);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> items)
        {
            await dbSet.AddRangeAsync(items);
        }

        public async Task<bool> AnyAsync()
        {
            return await dbSet.AnyAsync();
        }

        public virtual void Delete(int id)
        {
            TEntity item = dbSet.Find(id);
            if (item != null)
                dbSet.Remove(item);
        }

        public virtual async Task<TEntity> FindAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public virtual void Update(TEntity item)
        {
            context.Entry(item).State = EntityState.Modified;
        }

        #endregion Methods
    }
}