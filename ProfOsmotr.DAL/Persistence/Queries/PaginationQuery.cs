namespace ProfOsmotr.DAL
{
    /// <summary>
    /// Базовый запрос для постраничной выдачи списка элементов
    /// </summary>
    public class PaginationQuery
    {
        public int Page { get; set; }

        public int ItemsPerPage { get; set; }
    }
}