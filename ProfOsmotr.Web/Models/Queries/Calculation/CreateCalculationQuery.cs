﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProfOsmotr.Web.Models
{
    public class CreateCalculationQuery
    {
        [Required]
        [StringLength(70)]
        public string Name { get; set; }

        public IEnumerable<CreateCalculationSourceQuery> Sources { get; set; }
    }
}