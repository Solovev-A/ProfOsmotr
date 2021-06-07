using System;
using System.Collections.Generic;

namespace ProfOsmotr.Web.Models
{
    public class PatientResource
    {
        public int Id { get; private set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string PatronymicName { get; set; }

        public string Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Address { get; set; }

        public virtual IEnumerable<PatientCheckupStatusListItemResource> PreliminaryMedicalExaminations { get; set; }

        public virtual IEnumerable<PatientCheckupStatusListItemResource> ContingentCheckupStatuses { get; set; }
    }
}