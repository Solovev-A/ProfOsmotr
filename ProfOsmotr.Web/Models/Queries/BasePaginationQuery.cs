using System;
using System.ComponentModel.DataAnnotations;

namespace ProfOsmotr.Web.Models
{
    public class BasePaginationQuery
    {
        public int Page { get; set; } = 1;

        [Range(1, 100)]
        public int ItemsPerPage { get; set; } = 20;
    }
}