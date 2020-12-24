namespace ProfOsmotr.Web.Models.DataTables
{
    internal class DataTablesQuery
    {
        public int Start { get; set; }

        public int Length { get; set; }

        public string Search { get; set; }

        public dynamic OrderingSelector { get; set; }

        public bool Descending { get; set; }
    }
}