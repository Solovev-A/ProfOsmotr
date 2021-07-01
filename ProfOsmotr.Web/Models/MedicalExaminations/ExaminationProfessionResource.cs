using System.Collections.Generic;

namespace ProfOsmotr.Web.Models
{
    public class ExaminationProfessionResource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<ExaminationOrderItemResource> OrderItems { get; set; }
    }
}