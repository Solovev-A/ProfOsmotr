using System.Collections.Generic;

namespace ProfOsmotr.BL
{
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
    }
}