using System;

namespace ProfOsmotr.Web.Models
{
    public class PatientsListItemResource
    {
        public int Id { get; private set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string PatronymicName { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}