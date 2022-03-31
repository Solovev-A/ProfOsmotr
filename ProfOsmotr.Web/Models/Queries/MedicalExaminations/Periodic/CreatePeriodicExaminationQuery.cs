using System.ComponentModel.DataAnnotations;

namespace ProfOsmotr.Web.Models
{
    public class CreatePeriodicExaminationQuery
    {
        [Required]
        public int EmployerId { get; set; }

        [Required]
        public int ExaminationYear { get; set; }
    }
}