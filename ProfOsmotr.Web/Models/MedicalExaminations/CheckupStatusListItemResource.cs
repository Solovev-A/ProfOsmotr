using System;

namespace ProfOsmotr.Web.Models
{
    public class CheckupStatusListItemResource
    {
        public int Id { get; set; }

        public string Profession { get; set; }

        public string OrderItems { get; set; }

        public DateTime? DateOfCompletion { get; set; }

        public string Result { get; set; }

        public string Patient { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}