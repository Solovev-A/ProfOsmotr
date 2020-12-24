using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProfOsmotr.Web.Models
{
    public class UpdateOrderItemResource
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        public IEnumerable<int> Examinations { get; set; }
    }
}