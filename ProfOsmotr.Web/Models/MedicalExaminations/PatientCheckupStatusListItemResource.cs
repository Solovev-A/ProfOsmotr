using System;
using System.Collections.Generic;

namespace ProfOsmotr.Web.Models
{
    public class PatientCheckupStatusListItemResource
    {
        // Для предварительных осмотров передается id медицинского осмотра, для периодических - id чекапа
        public int Id { get; set; }

        public string Employer { get; set; }

        public string Profession { get; set; }

        public IEnumerable<string> OrderItems { get; set; }

        public string DateOfCompletion { get; set; }

        public string Result { get; set; }
    }
}