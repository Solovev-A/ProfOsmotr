using ProfOsmotr.DAL;
using System;
using System.ComponentModel.DataAnnotations;

namespace ProfOsmotr.Web.Models
{
    public class CreatePatientQuery
    {
        [Required]
        [StringLength(70)]
        public string LastName { get; set; }

        [Required]
        [StringLength(70)]
        public string FirstName { get; set; }

        [StringLength(70)]
        public string PatronymicName { get; set; }

        [Required]
        public GenderId Gender { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [StringLength(500)]
        public string Address { get; set; }
    }
}