using System.Collections.Generic;

namespace ProfOsmotr.Web.Models
{
    public class ProfessionSearchResultResource
    {
        public IEnumerable<ProfessionResource> Items { get; set; }

        public IEnumerable<ProfessionResource> Suggestions { get; set; }
    }
}