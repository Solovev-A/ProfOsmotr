using System;
using System.Collections.Generic;

namespace ProfOsmotr.Web.Models
{
    public class PreliminaryMedicalExaminationsListItemResource
    {
        public int Id { get; set; }

        public string Profession { get; set; }

        public IEnumerable<string> OrderItems { get; set; }

        public string DateOfCompletion { get; set; }

        public bool IsCompleted { get; set; }

        public CheckupStatusPatientResource Patient { get; set; }

        public string EmployerName { get; set; }
    }
}