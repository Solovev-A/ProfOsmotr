using ProfOsmotr.DAL;
using System;
using System.Collections.Generic;

namespace ProfOsmotr.BL
{
    public class UpdateContingentGroupMedicalReportQuery
    {
        public IEnumerable<int> CheckupStatuses { get; set; }

        public CheckupResultId? CheckupResultId { get; set; }

        public bool CheckupStarted { get; set; }

        public DateTime? DateOfCompletion { get; set; }
    }
}