namespace ProfOsmotr.DAL
{
    /// <summary>
    /// Представляет элемент результата расчета медицинского осмотра
    /// </summary>
    public class CalculationResultItem
    {
        /// <summary>
        /// Идентификатор элемента
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Идентификатор родительского расчета медицинского осмотра
        /// </summary>
        public int CalculationId { get; set; }

        /// <summary>
        /// Родительский расчет медицинского осмотра
        /// </summary>
        public virtual Calculation Calculation { get; set; }

        /// <summary>
        /// Медицинская услуга
        /// </summary>
        public virtual Service Service { get; set; }

        /// <summary>
        /// Количество медицинских услуг
        /// </summary>
        public int Amount { get; set; }
    }
}