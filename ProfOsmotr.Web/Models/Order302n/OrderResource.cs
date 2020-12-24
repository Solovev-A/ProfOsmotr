using System;
using System.Collections.Generic;

namespace ProfOsmotr.Web.Models
{
    public class OrderResource
    {
        public OrderResource(IEnumerable<AnnexResource> annexes)
        {
            Annexes = annexes ?? throw new ArgumentNullException(nameof(annexes));
        }

        public IEnumerable<AnnexResource> Annexes { get; set; }
    }
}