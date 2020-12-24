using System.Collections.Generic;

namespace ProfOsmotr.BL
{
    /// <summary>
    /// Представляет исходные данные для обновления пункта приказа
    /// </summary>
    public class UpdateOrderItemRequest
    {
        /// <summary>
        /// Идентификатор существующего пункта, который подвергается изменению
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование пункта
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Перечисление идентификаторов обследований по приказу
        /// </summary>
        public IEnumerable<int> OrderExaminationIdentifiers { get; set; }
    }
}