using System.ComponentModel.DataAnnotations;

namespace ProfOsmotr.Web.Models
{
    public class CreateEmployerDepartmentQuery
    {
        [Required]
        [StringLength(500)]
        public string Name { get; set; }
    }
}