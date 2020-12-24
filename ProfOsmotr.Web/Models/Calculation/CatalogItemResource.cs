using System;

namespace ProfOsmotr.Web.Models
{
    public class CatalogItemResource
    {
        public int Id { get; set; }

        public int OrderExaminationId { get; set; }

        public string OrderExaminationName { get; set; }

        public string FullName { get; set; }

        public string Code { get; set; }

        public decimal Price { get; set; }

        public int ServiceAvailabilityGroupId { get; set; }

        public string ServiceAvailabilityGroupName { get; set; }

        public DateTime UpdateTime { get; set; }
    }
}