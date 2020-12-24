using System.Collections.Generic;

namespace ProfOsmotr.DAL
{
    /// <summary>
    /// Представляет приложение приказа
    /// </summary>
    public class OrderAnnex
    {
        /// <summary>
        /// Идентификатор приложения
        /// </summary>
        public OrderAnnexId Id { get; set; }

        /// <summary>
        /// Название приложения
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Коллекция пунктов приказа, входящих в приложение
        /// </summary>
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}