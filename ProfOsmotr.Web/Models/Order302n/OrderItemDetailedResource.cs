using System.Collections.Generic;

namespace ProfOsmotr.Web.Models
{
    public class OrderItemDetailedResource : OrderItemResource
    {
        public int AnnexId { get; set; }

        public IEnumerable<int> OrderExaminations { get; set; }
    }
}