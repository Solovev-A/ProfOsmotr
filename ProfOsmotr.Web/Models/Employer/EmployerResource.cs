using System.Collections.Generic;

namespace ProfOsmotr.Web.Models
{
    public class EmployerResource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string HeadLastName { get; set; }

        public string HeadFirstName { get; set; }

        public string HeadPatronymicName { get; set; }

        public string HeadPosition { get; set; }

        public IEnumerable<EmployerPeriodicMedicalExaminationResource> PeriodicMedicalExaminations { get; set; }
    }
}