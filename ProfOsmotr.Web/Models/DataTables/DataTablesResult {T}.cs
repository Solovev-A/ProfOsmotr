using System.Collections.Generic;

namespace ProfOsmotr.Web.Models.DataTables
{
    public class DataTablesResult<T>
    {
        public int Draw { get; set; }

        public int RecordsTotal { get; set; }

        public int RecordsFiltered { get; set; }

        public IEnumerable<T> Data { get; set; }
    }
}