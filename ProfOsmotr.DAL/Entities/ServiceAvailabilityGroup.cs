namespace ProfOsmotr.DAL
{
    /// <summary>
    /// Представляет группу доступности медицинской услуги
    /// </summary>
    public class ServiceAvailabilityGroup
    {
        /// <summary>
        /// Идентификатор группы доступности услуги
        /// </summary>
        public ServiceAvailabilityGroupId Id { get; set; }

        /// <summary>
        /// Название группы доступности услугиы
        /// </summary>
        public string Name { get; set; }
    }
}