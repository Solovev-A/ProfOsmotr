using System.Collections.Generic;

namespace ProfOsmotr.Web.Models
{
    public class AnnexResource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<OrderItemDetailedResource> OrderItems { get; set; }
    }
}