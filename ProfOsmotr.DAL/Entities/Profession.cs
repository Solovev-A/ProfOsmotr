using System.Collections.Generic;

namespace ProfOsmotr.DAL
{
    /// <summary>
    /// Представляет профессию, работники которой подлежат медицинскому осмотру
    /// </summary>
    public class Profession
    {
        /// <summary>
        /// Идентификатор профессии
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Название профессии
        /// </summary>
        public string Name { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();

        public virtual ICollection<IndividualCheckupStatus> IndividualCheckupStatuses { get; } = new List<IndividualCheckupStatus>();

        public virtual ICollection<ContingentCheckupStatus> ContingentCheckupStatuses { get; } = new List<ContingentCheckupStatus>();
    }
}