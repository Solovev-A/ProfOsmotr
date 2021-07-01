using System;
using System.Collections.Generic;

namespace ProfOsmotr.Web.Models
{
    public class PreliminaryMedicalExaminationsListItemResource
    {
        public int Id { get; set; }

        public string Profession { get; set; }

        public IEnumerable<string> OrderItems { get; set; }

        public DateTime? DateOfCompletion { get; set; }

        public bool IsCompleted { get; set; }

        public ExaminationPatientResource Patient { get; set; }

        public string EmployerName { get; set; }
    }
}