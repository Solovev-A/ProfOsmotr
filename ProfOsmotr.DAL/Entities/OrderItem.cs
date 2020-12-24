using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProfOsmotr.DAL
{
    /// <summary>
    /// Представляет пункт приказа
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// Идентификатор пункта
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Идентификатор приложения приказа
        /// </summary>
        public OrderAnnexId OrderAnnexId { get; set; }

        /// <summary>
        /// Приложение приказа
        /// </summary>
        public virtual OrderAnnex OrderAnnex { get; set; }

        /// <summary>
        /// Ключ, номер пункта по приказу
        /// </summary>
        [Required]
        public string Key { get; set; }

        /// <summary>
        /// Название пункта
        /// </summary>
        public string Name { get; set; }

        public virtual ICollection<OrderItemOrderExamination> OrderItemOrderExaminations { get; } = new List<OrderItemOrderExamination>();

        public virtual ICollection<ProfessionOrderItem> ProfessionOrderItems { get; } = new List<ProfessionOrderItem>();

        /// <summary>
        /// Значение статуса удаления элемента
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}