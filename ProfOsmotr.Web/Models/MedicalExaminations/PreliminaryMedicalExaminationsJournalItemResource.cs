using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfOsmotr.Web.Models
{
    public class PreliminaryMedicalExaminationsJournalItemResource
    {
        public int Id { get; set; }

        public int RegistrationJournalEntryNumber { get; set; }

        public string DateOfCompletion { get; set; }

        public JournalPatientResource Patient { get; set; }
    }
}
