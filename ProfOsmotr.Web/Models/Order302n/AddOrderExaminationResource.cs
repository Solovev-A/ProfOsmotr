﻿using System.ComponentModel.DataAnnotations;

namespace ProfOsmotr.Web.Models
{
    public class AddOrderExaminationResource
    {
        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string DefaultServiceCode { get; set; }

        [Required]
        [StringLength(500)]
        public string DefaultServiceFullName { get; set; }

        public int TargetGroupId { get; set; }
    }
}