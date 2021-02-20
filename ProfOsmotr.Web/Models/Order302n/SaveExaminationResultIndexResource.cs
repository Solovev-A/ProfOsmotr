using System.ComponentModel.DataAnnotations;

namespace ProfOsmotr.Web.Models
{
    public class SaveExaminationResultIndexResource
    {        
        [Required]
        [StringLength(70)]
        public string Title { get; set; }

        [StringLength(70)]
        public string UnitOfMeasure { get; set; }
    }
}