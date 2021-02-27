using System.ComponentModel.DataAnnotations;

namespace ProfOsmotr.Web.Models
{
    public class UpdateOrderExaminationResource
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string DefaultServiceCode { get; set; }

        [Required]
        [StringLength(500)]
        public string DefaultServiceFullName { get; set; }

        public int TargetGroupId { get; set; }

        public bool IsMandatory { get; set; }
    }
}