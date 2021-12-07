using System.ComponentModel.DataAnnotations;

namespace ProfOsmotr.Web.Models
{
    public class SearchPaginationQuery : BasePaginationQuery
    {
        [Required]
        [MinLength(3, ErrorMessage = "Слишком короткий запрос")]
        public string Search { get; set; }
    }
}