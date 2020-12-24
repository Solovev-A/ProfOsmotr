using System.Collections.Generic;

namespace ProfOsmotr.Web.Models
{
    public class OrderItemsListResource
    {
        public ICollection<OrderItemResource> Annex1 { get; set; } = new List<OrderItemResource>();

        public ICollection<OrderItemResource> Annex2 { get; set; } = new List<OrderItemResource>();
    }
}