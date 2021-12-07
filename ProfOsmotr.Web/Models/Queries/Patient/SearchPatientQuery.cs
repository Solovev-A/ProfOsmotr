namespace ProfOsmotr.Web.Models
{
    public class SearchPatientQuery : SearchPaginationQuery
    {
        public int? EmployerId { get; set; }
    }
}