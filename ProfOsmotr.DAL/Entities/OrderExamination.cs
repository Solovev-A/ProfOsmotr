using System.Collections.Generic;

namespace ProfOsmotr.DAL
{
    /// <summary>
    /// Представляет обследование по приказу
    /// </summary>
    public class OrderExamination
    {
        /// <summary>
        /// Идентификатор обследования по приказу
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Название обследования
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Признак обязательности обследования для любого медицинского осмотра
        /// </summary>
        public bool IsMandatory { get; set; }

        /// <summary>
        /// Идентификатор целевой группы
        /// </summary>
        public TargetGroupId TargetGroupId { get; set; }

        /// <summary>
        /// Целевая группа обследования
        /// </summary>
        public virtual TargetGroup TargetGroup { get; set; }

        /// <summary>
        /// Идентификатор подробного описания соответстсвующей обследованию услуги по умолчанию
        /// </summary>
        public int DefaultServiceDetailsId { get; set; }

        /// <summary>
        /// Подробное описание соответстсвующей обследованию услуги по умолчанию
        /// </summary>
        public virtual ServiceDetails DefaultServiceDetails { get; set; }

        /// <summary>
        /// Коллекция услуг, соответствующих обследованию
        /// </summary>
        public virtual ICollection<Service> Services { get; } = new List<Service>();

        /// <summary>
        /// Коллекция актуальных услуг, соответствующих обследованию
        /// </summary>
        public virtual ICollection<ActualClinicService> ActualClinicServices { get; } = new List<ActualClinicService>();

        public virtual ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();

        public virtual ICollection<ExaminationResultIndex> ExaminationResultIndexes { get; } = new List<ExaminationResultIndex>();        
    }
}