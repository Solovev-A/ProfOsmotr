using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProfOsmotr.Web.Models
{
    public class CreateProfessionResource
    {
        [Required]
        [StringLength(70)]
        public string Name { get; set; }

        public IEnumerable<int> OrderItems { get; set; }

        public int NumberOfPersons { get; set; }

        public int NumberOfPersonsOver40 { get; set; }

        public int NumberOfWomen { get; set; }

        public int NumberOfWomenOver40 { get; set; }
    }
}