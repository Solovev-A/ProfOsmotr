using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProfOsmotr.DAL.Abstractions
{
    /// <summary>
    /// Представляет абстракцию хранилища, выполняющего запросы на постраничную выдачу элементов
    /// </summary>
    /// <typeparam name="TEntity">Тип сущностей хранилища</typeparam>
    public interface IQueryAwareRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Предоставляет сущности, удовлетворяющие заданным параметрам постраничной выдачи без сортировки
        /// </summary>
        /// <param name="start">Позиция первого элемента выдачи</param>
        /// <param name="length">Количество запрашиваемых элементов</param>
        /// <param name="search">Строка поискового запроса</param>
        /// <param name="customFilter">Дополнительное условие фильтрации элементов выдачи</param>
        /// <returns></returns>
        Task<QueryResult<TEntity>> ExecuteQuery(
            int start = 0,
            int length = 10,
            string search = null,
            Expression<Func<TEntity, bool>> customFilter = null);

        /// <summary>
        /// Предоставляет сущности, удовлетворяющие заданным параметрам постраничной выдачи с сортировкой
        /// </summary>
        /// <typeparam name="TKey">
        /// Тип свойства, по которому будет производиться сортировка выдачи
        /// </typeparam>
        /// <param name="orderingSelector">
        /// Селектор свойства, по которому будет производиться сортировка выдачи
        /// </param>
        /// <param name="descending">true если сортировка по убыванию, false - по возрастанию</param>
        /// <param name="start">Позиция первого элемента выдачи</param>
        /// <param name="length">Количество запрашиваемых элементов</param>
        /// <param name="search">Строка поискового запроса</param>
        /// <param name="customFilter">Дополнительное условие фильтрации элементов выдачи</param>
        /// <returns></returns>
        Task<QueryResult<TEntity>> ExecuteQuery<TKey>(
            Expression<Func<TEntity, TKey>> orderingSelector,
            bool descending,
            int start = 0,
            int length = 10,
            string search = null,
            Expression<Func<TEntity, bool>> customFilter = null);
    }
}