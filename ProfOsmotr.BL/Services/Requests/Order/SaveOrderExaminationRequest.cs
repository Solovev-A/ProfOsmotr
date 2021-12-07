namespace ProfOsmotr.BL
{
    /// <summary>
    /// Представляет исходные данные для сохранения медицинского обследования
    /// </summary>
    public class SaveOrderExaminationRequest
    {
        /// <summary>
        /// Название обследования
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор целевой группы
        /// </summary>
        public int TargetGroupId { get; set; }

        /// <summary>
        /// Код услуги, используемой в отношении обследования, по умолчаюнию
        /// </summary>
        public string DefaultServiceCode { get; set; }

        /// <summary>
        /// Полной наименование услуги, используемой в отношении обследования, по умолчаюнию
        /// </summary>
        public string DefaultServiceFullName { get; set; }

        /// <summary>
        /// Признак обязательности обследования при любом медицинском осмотре
        /// </summary>
        public bool IsMandatory { get; set; }
    }
}