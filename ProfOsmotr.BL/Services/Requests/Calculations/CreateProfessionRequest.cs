using System.Collections.Generic;

namespace ProfOsmotr.BL
{
    /// <summary>
    /// Исходные данные для создания профессии, в отношении которой планируется проведение
    /// медицинского осмотра
    /// </summary>
    public class CreateProfessionRequest
    {
        /// <summary>
        /// Название профессии
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Перечисление идентификаторов пунктов приказа, соответствующих условиям труда,
        /// свойственным данной профессии
        /// </summary>
        public IEnumerable<int> OrderItemIdentifiers { get; set; }

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