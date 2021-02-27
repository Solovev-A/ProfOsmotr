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
        /// Ключ, номер пункта по приказу
        /// </summary>
        [Required]
        public string Key { get; set; }

        /// <summary>
        /// Название пункта
        /// </summary>
        public string Name { get; set; }

        public virtual ICollection<OrderExamination> OrderExaminations { get; } = new List<OrderExamination>();

        public virtual ICollection<Profession> Professions { get; } = new List<Profession>();

        /// <summary>
        /// Значение статуса удаления элемента
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}