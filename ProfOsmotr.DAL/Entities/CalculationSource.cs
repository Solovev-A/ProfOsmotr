namespace ProfOsmotr.DAL
{
    /// <summary>
    /// Представляет первичные исходные данные для производства расчета медицинского осмотра
    /// </summary>
    public class CalculationSource
    {
        /// <summary>
        /// Идентификатор исходных данных
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Идентификатор расчета, произведенного по текущим данным
        /// </summary>
        public int CalculationId { get; set; }

        /// <summary>
        /// Расчет, произведенный по текущим данным
        /// </summary>
        public virtual Calculation Calculation { get; set; }

        /// <summary>
        /// Идентификатор профессии, в отношении которой планируется медицинский осмотр
        /// </summary>
        public int ProfessionId { get; set; }

        /// <summary>
        /// Профессия, в отношении которой планируется медицинский осмотр
        /// </summary>
        public virtual Profession Profession { get; set; }

        /// <summary>
        /// Общая численность работников
        /// </summary>
        public int NumberOfPersons { get; set; }

        /// <summary>
        /// Численность работников старше 40 лет
        /// </summary>
        public int NumberOfPersonsOver40 { get; set; }

        /// <summary>
        /// Численность работников женского пола
        /// </summary>
        public int NumberOfWomen { get; set; }

        /// <summary>
        /// Численность работников женского пола старше 40 лет
        /// </summary>
        public int NumberOfWomenOver40 { get; set; }
    }
}