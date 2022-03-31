using System.Collections.Generic;

namespace ProfOsmotr.Web.Models
{
    public class CheckupExaminationResultIndexResource
    {
        public string ExaminationName { get; set; }

        public IEnumerable<CheckupIndexValueResource> CheckupIndexValues { get; set; }
    }
}