using ProfOsmotr.Web.Infrastructure.Validation;
using System.ComponentModel.DataAnnotations;

namespace ProfOsmotr.Web.Models
{
    public class BaseClinicDetailsQuery
    {
        [Required]
        [StringLength(500)]
        public string FullName { get; set; }

        [Required]
        [StringLength(500)]
        public string ShortName { get; set; }

        [Required]
        [StringLength(500)]
        public string Address { get; set; }

        [CustomPhone]
        public string Phone { get; set; }

        [CustomEmail]
        public string Email { get; set; }
    }
}