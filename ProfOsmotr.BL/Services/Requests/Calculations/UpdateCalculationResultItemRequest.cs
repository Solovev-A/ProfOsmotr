namespace ProfOsmotr.BL
{
    /// <summary>
    /// Исходные данные для обновления результатов расчета
    /// </summary>
    public class UpdateCalculationResultItemRequest
    {
        /// <summary>
        /// Идентификатор существующего элемента результата расчета
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Стоимость услуги
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Количество услуг
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Идентификатор группы доступности услуги
        /// </summary>
        public int GroupId { get; set; }
    }
}