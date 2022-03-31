using System;
using System.ComponentModel.DataAnnotations;

namespace ProfOsmotr.Web.Models
{
    public class UpdateCatalogItemQuery
    {
        public int OrderExaminationId { get; set; }

        [Required]
        [StringLength(500)]
        public string FullName { get; set; }

        [Required]
        [StringLength(20)]
        public string Code { get; set; }

        [Range(typeof(decimal), "0", "999999999999")]
        public decimal Price { get; set; }

        public int ServiceAvailabilityGroupId { get; set; }
    }
}