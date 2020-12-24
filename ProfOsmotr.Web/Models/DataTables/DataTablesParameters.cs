namespace ProfOsmotr.Web.Models.DataTables
{
    public class DataTablesParameters
    {
        public int Draw { get; set; }

        public DTColumn[] Columns { get; set; }

        public DTOrder[] Order { get; set; }

        public int Start { get; set; }

        public int Length { get; set; }

        public DTSearch Search { get; set; }

        public bool OrderingRequired => Order?.Length > 0;

        public bool SearchRequired => !(string.IsNullOrEmpty(Search?.Value) || string.IsNullOrWhiteSpace(Search?.Value));
    }
}