using System.Collections.Generic;

namespace ProfOsmotr.DAL
{
    /// <summary>
    /// Представляет результат запроса с постраничной выдачей элементов
    /// </summary>
    /// <typeparam name="T">Тип элементов данных</typeparam>
    public class QueryResult<T>
    {
        /// <summary>
        /// Общее количество элементов
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// Перечисление элементов, удовлетворяющих запросу
        /// </summary>
        public IEnumerable<T> Data { get; set; }
    }
}