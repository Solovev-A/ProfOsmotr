using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProfOsmotr.Web.Models
{
    public class CreateProfessonQuery
    {
        [Required]
        [StringLength(70)]
        public string Name { get; set; }

        public IEnumerable<int> OrderItems { get; set; }
    }
}