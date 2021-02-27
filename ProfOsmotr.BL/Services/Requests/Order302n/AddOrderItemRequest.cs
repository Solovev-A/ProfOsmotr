using System.Collections.Generic;

namespace ProfOsmotr.BL
{
    /// <summary>
    /// Представляет исходные данные для добавления элемента приказа
    /// </summary>
    public class AddOrderItemRequest
    {
        /// <summary>
        /// Ключ, номер пункта по приказу
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Наименование пункта
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Идентификаторы существующих обследований, предполагаемых данным пунктом
        /// </summary>
        public IEnumerable<int> OrderExaminationIdentifiers { get; set; }
    }
}