using System.Collections.Generic;

namespace ProfOsmotr.Web.Models
{
    public class ContingentCheckupStatusListItemResource
    {
        public int Id { get; set; }

        public ExaminationPatientResource Patient { get; set; }

        public string Profession { get; set; }

        public IEnumerable<string> OrderItems { get; set; }

        public CheckupResultResource Result { get; set; }

        public string DateOfCompletion { get; set; }
    }
}