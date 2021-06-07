using System;
using System.Collections.Generic;
using System.Text;

namespace ProfOsmotr.BL
{
    public class ListPatientsRequest
    {
        public int ClinicId { get; set; }

        public int Page { get; set; }

        public int ItemsPerPage { get; set; }

        public string Search { get; set; }
    }
}
