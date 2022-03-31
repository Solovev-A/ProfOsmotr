using System.Collections.Generic;

namespace ProfOsmotr.Web.Models
{
    public class PagedResource<T>
    {
        public int TotalCount { get; set; }

        public IEnumerable<T> Items { get; set; }
    }
}