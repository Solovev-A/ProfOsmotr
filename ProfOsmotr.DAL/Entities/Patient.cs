using System;
using System.Collections.Generic;

namespace ProfOsmotr.DAL
{
    public class Patient
    {
        public int Id { get; private set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string PatronymicName { get; set; }

        public GenderId GenderId { get; set; }

        public virtual Gender Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Address { get; set; }

        public int ClinicId { get; set; }

        public virtual Clinic Clinic { get; set; }

        public virtual ICollection<IndividualMedicalExamination> IndividualMedicalExaminations { get; } = new List<IndividualMedicalExamination>();
        
        public virtual ICollection<ContingentMedicalExamination> ContingentMedicalExaminations { get; } = new List<ContingentMedicalExamination>();
    }
}