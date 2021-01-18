using System.Collections.Generic;

namespace ProfOsmotr.DAL
{
    /// <summary>
    /// Представляет класс заболеваний, в соответствии с Международной классификации болезней 10-го
    /// пересмотра (МКБ-10)
    /// </summary>
    public class ICD10Chapter
    {
        public int Id { get; set; }

        /// <summary>
        /// Представляет блоки рубрик класса заболеваний по МКБ-10
        /// </summary>
        public string Block { get; set; }

        /// <summary>
        /// Представляет заголовок класса заболеваний по МКБ-10
        /// </summary>
        public string Title { get; set; }
    }
}