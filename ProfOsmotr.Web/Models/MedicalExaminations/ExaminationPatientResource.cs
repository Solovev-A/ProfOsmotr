﻿namespace ProfOsmotr.Web.Models
{
    public class ExaminationPatientResource
    {
        public int Id { get; private set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string PatronymicName { get; set; }

        public string DateOfBirth { get; set; }
    }
}