using Microsoft.EntityFrameworkCore;
using ProfOsmotr.DAL.Abstractions;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProfOsmotr.DAL
{
    public abstract class QueryAwareRepository<TEntity> : EFRepository<TEntity>, IQueryAwareRepository<TEntity>
        where TEntity : class
    {
        #region Constructors

        public QueryAwareRepository(ProfContext context) : base(context)
        {
        }

        #endregion Constructors

        #region Methods

        public async Task<QueryResult<TEntity>> ExecuteQuery(
            int start = 0,
            int length = 10,
            string search = null,
            Expression<Func<TEntity, bool>> customFilter = null)
        {
            // Перегрузка метода для ситуации, когда сортировка не нужна, и неизвестен тип TEntity.

            return await ExecuteQuery<object>(null, true, start, length, search, customFilter);
        }

        public async Task<QueryResult<TEntity>> ExecuteQuery<TKey>(
            Expression<Func<TEntity, TKey>> orderingSelector,
            bool descending,
            int start = 0,
            int length = 10,
            string search = null,
            Expression<Func<TEntity, bool>> customFilter = null)
        {
            IQueryable<TEntity> dataQuery = GetInitialQuery();
            Expression<Func<TEntity, bool>> filterCondition = string.IsNullOrEmpty(search) ? null : GetSearchFilterCondition(search);
            filterCondition = ApplyCustomFilter(filterCondition, customFilter);
            var result = await ProcessQuery(dataQuery, start, length, filterCondition, orderingSelector, descending);

            return result;
        }

        // Используется для создания базового запроса с включением необходимых сущностей
        protected abstract IQueryable<TEntity> GetInitialQuery();

        // Используется для получения индивидуального для каждой сущности условия поискового фильтра
        protected abstract Expression<Func<TEntity, bool>> GetSearchFilterCondition(string search);

        private Expression<Func<TEntity, bool>> ApplyCustomFilter(Expression<Func<TEntity, bool>> filterCondition,
                                                                                  Expression<Func<TEntity, bool>> customFilter)
        {
            if (customFilter == null)
                return filterCondition;

            if (filterCondition == null)
                return customFilter;

            return filterCondition.AndAlso(customFilter);
        }

        private IQueryable<T> ApplyFilter<T>(IQueryable<T> dataQuery, Expression<Func<T, bool>> predicate)
        {
            if (predicate != null)
            {
                dataQuery = dataQuery.Where(predicate);
            }
            return dataQuery;
        }

        private IQueryable<T> ApplyOrder<T, TKey>(IQueryable<T> dataQuery, Expression<Func<T, TKey>> selector, bool descending)
        {
            if (selector != null)
            {
                if (descending)
                    dataQuery = dataQuery.OrderByDescending(selector);
                else
                    dataQuery = dataQuery.OrderBy(selector);
            }
            return dataQuery;
        }

        private async Task<T[]> GetPagedData<T>(IQueryable<T> query, int start, int length)
        {
            return await query
                .Skip(start)
                .Take(length)
                .ToArrayAsync();
        }

        private async Task<QueryResult<TEntity>> ProcessQuery<TEntity, TKey>(
            IQueryable<TEntity> dataQuery,
            int start,
            int length,
            Expression<Func<TEntity, bool>> filterCondition,
            Expression<Func<TEntity, TKey>> orderingSelector,
            bool descending)
        {
            dataQuery = ApplyFilter(dataQuery, filterCondition);
            dataQuery = ApplyOrder(dataQuery, orderingSelector, descending);

            var count = await dataQuery.CountAsync();
            var data = await GetPagedData(dataQuery, start, length);

            return new QueryResult<TEntity>()
            {
                Items = data,
                TotalCount = count
            };
        }

        #endregion Methods
    }
}