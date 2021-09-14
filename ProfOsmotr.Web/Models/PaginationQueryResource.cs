using System;
using System.ComponentModel.DataAnnotations;

namespace ProfOsmotr.Web.Models
{
    public class PaginationQueryResource
    {
        public int Page { get; set; } = 1;

        [Range(1, 100)]
        public int ItemsPerPage { get; set; } = 20;
    }
}