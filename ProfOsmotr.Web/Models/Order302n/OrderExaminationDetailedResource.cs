﻿namespace ProfOsmotr.Web.Models
{
    public class OrderExaminationDetailedResource : OrderExaminationResource
    {
        public int TargetGroupId { get; set; }

        public bool IsMandatory { get; set; }

        public ServiceDetailsResource DefaultServiceDetails { get; set; }
    }
}