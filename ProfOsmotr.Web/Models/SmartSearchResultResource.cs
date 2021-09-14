using System.Collections.Generic;

namespace ProfOsmotr.Web.Models
{
    public class SmartSearchResultResource<TResource>
    {
        public IEnumerable<TResource> Items { get; set; }

        public IEnumerable<TResource> Suggestions { get; set; }
    }
}