using System.Collections.Generic;

namespace ProfOsmotr.Web.Models
{
    public class OrderItemDetailedResource : OrderItemResource
    {
        public IEnumerable<int> OrderExaminations { get; set; }
    }
}