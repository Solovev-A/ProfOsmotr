namespace ProfOsmotr.BL
{
    /// <summary>
    /// Представляет исходные данные для управления статусом медицинской организации
    /// </summary>
    public class ManageClinicRequest
    {
        /// <summary>
        /// Идентификатор медицинской организации
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Отражает необходимость блокировки медицинской организации
        /// </summary>
        public bool NeedBlock { get; set; }
    }
}