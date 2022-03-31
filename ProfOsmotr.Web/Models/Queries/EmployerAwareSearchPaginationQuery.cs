namespace ProfOsmotr.Web.Models
{
    public class EmployerAwareSearchPaginationQuery : SearchPaginationQuery
    {
        public int? EmployerId { get; set; }
    }
}