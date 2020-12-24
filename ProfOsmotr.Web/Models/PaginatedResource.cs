using System.Collections.Generic;

namespace ProfOsmotr.Web.Models
{
    public class PaginatedResource<T>
    {
        public int TotalCount { get; set; }

        public int TotalPages { get; set; }

        public int CurrentPage { get; set; }

        public IEnumerable<T> Items { get; set; }
    }
}