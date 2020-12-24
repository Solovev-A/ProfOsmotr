namespace ProfOsmotr.DAL
{
    /// <summary>
    /// Представляет подробное описание услуги
    /// </summary>
    public class ServiceDetails
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Код медицинской услуги
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Полное наименование медицинской услуги
        /// </summary>
        public string FullName { get; set; }
    }
}