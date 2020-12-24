namespace ProfOsmotr.DAL
{
    /// <summary>
    /// Представляет запрос для постраничной выдачи списка расчетов
    /// </summary>
    public class CalculationsPaginationQuery : PaginationQuery
    {
        /// <summary>
        /// Идентификатор медицинской организации, список расчетов которой должен быть выдан
        /// </summary>
        public int ClinicId { get; set; }
    }
}