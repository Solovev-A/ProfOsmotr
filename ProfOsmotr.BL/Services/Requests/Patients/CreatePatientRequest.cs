using ProfOsmotr.DAL;
using System;

namespace ProfOsmotr.BL
{
    public class CreatePatientRequest
    {
        public int ClinicId { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string PatronymicName { get; set; }

        public GenderId Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Address { get; set; }
    }
}