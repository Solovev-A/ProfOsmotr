using System.Collections.Generic;

namespace ProfOsmotr.DAL.Models
{
    /// <summary>
    /// Представляет одну из страниц списка элементов типа <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T">Тип элементов списка</typeparam>
    public class PaginatedResult<T>
    {
        /// <summary>
        /// Общее количество элементов
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Общее количество страниц
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Порядковый номер текущей страницы
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Перечисление элементов текущей страницы
        /// </summary>
        public IEnumerable<T> Items { get; set; }
    }
}