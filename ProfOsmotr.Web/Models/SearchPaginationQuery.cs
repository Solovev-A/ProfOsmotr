﻿using System.ComponentModel.DataAnnotations;

namespace ProfOsmotr.Web.Models
{
    public class SearchPaginationQuery : PaginationQueryResource
    {
        [MinLength(3, ErrorMessage = "Слишком короткий запрос")]
        public string Search { get; set; }
    }
}