using System.Collections.Generic;

namespace ProfOsmotr.Web.Models
{
    public class ProfessionResource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> OrderItems { get; set; }
    }
}