using System.ComponentModel.DataAnnotations;

namespace ProfOsmotr.Web.Models
{
    public class CreateContingentCheckupStatusQuery
    {
        [Required]
        public int PatientId { get; set; }

        public int? EmployerDepartmentId { get; set; }

        public int? ProfessionId { get; set; }
    }
}