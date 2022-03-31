using ProfOsmotr.DAL;
using System;
using System.ComponentModel.DataAnnotations;

namespace ProfOsmotr.BL
{
    public class PatchPatientQuery : PatchDtoBase
    {
        [StringLength(70)]
        public string LastName { get; set; }

        [StringLength(70)]
        public string FirstName { get; set; }

        [StringLength(70)]
        public string PatronymicName { get; set; }

        public GenderId? Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        [StringLength(500)]
        public string Address { get; set; }
    }
}