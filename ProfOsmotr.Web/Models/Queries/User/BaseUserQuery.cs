using ProfOsmotr.Web.Infrastructure.Validation;
using System.ComponentModel.DataAnnotations;

namespace ProfOsmotr.Web.Models
{
    public class BaseUserQuery
    {
        [Password]
        public string Password { get; set; }

        [Required]
        [StringLength(70)]
        public string Name { get; set; }

        [Required]
        [StringLength(70)]
        public string Position { get; set; }
    }
}