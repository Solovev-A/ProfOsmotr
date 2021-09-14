using System.Collections.Generic;

namespace ProfOsmotr.BL.Models
{
    public class SmartSearchResult<T>
    {
        public IEnumerable<T> Items { get; set; }

        public IEnumerable<T> Suggestions { get; set; }
    }
}