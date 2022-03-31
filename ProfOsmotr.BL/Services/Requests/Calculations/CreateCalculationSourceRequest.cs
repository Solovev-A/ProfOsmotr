namespace ProfOsmotr.BL
{
    /// <summary>
    /// Исходные данные для создания профессии, в отношении которой планируется проведение
    /// медицинского осмотра
    /// </summary>
    public class CreateCalculationSourceRequest
    {
        public CreateProfessionRequest Profession { get; set; }

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