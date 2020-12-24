using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProfOsmotr.DAL.Abstractions
{
    /// <summary>
    /// Представляет хранилище объектов типа <typeparamref name="TEntity"/>
    /// </summary>
    /// <typeparam name="TEntity">Тип хранимых объектов</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Добавляет элемент <paramref name="item"/> в хранилище
        /// </summary>
        /// <param name="item">Сущность для добавления</param>
        Task AddAsync(TEntity item);

        /// <summary>
        /// Добавляет несколько элементов в хранилище
        /// </summary>
        /// <param name="items">Элементы для добавления</param>
        Task AddRangeAsync(IEnumerable<TEntity> items);

        /// <summary>
        /// Проверяет, есть ли хотя бы один элемент в хранилище
        /// </summary>
        /// <returns>true, если элемет есть, false - если хранилище пусто</returns>
        Task<bool> AnyAsync();

        /// <summary>
        /// Удаляет сущность с идентификатором <paramref name="id"/> из хранилища
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);

        /// <summary>
        /// Предоставляет сущность с идентификатором <paramref name="id"/>
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <returns>Объект сущности, если он был найден, иначе - null</returns>
        Task<TEntity> FindAsync(int id);

        /// <summary>
        /// Предоставляет сущности, отсеянные на основе <paramref name="predicate"/>
        /// </summary>
        /// <param name="predicate">Условие отбора элементов</param>
        /// <returns>Перечисление сущностей, удовлетворяющих условию</returns>
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Предоставляет первую сущность, удовлетворяющую условию <paramref name="predicate"/>
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>Первую сущность, удовлетворяющую условию, либо null, если таковой нет</returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Предоставляет все сущности, содержащиеся в хранилище
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Обновляет сущность <paramref name="item"/>
        /// </summary>
        /// <param name="item">Сущность для обновления</param>
        void Update(TEntity item);
    }
}