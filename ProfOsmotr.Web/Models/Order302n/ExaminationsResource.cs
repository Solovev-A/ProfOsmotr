using System;
using System.Collections.Generic;

namespace ProfOsmotr.Web.Models
{
    public class ExaminationsResource
    {
        public ExaminationsResource(IEnumerable<OrderExaminationDetailedResource> orderExaminations,
                                    IEnumerable<TargetGroupResource> targetGroups)
        {
            OrderExaminations = orderExaminations ?? throw new ArgumentNullException(nameof(orderExaminations));
            TargetGroups = targetGroups ?? throw new ArgumentNullException(nameof(targetGroups));
        }

        public IEnumerable<OrderExaminationDetailedResource> OrderExaminations { get; set; }

        public IEnumerable<TargetGroupResource> TargetGroups { get; set; }
    }
}