using System.ComponentModel.DataAnnotations;

namespace ProfOsmotr.Web.Models
{
    public class UpdateCalculationResultItemResource
    {
        public int Id { get; set; }

        [Range(typeof(decimal), "0", "999999999999")]
        public decimal Price { get; set; }

        [Range(0, 999999999999)]
        public int Amount { get; set; }

        public int GroupId { get; set; }
    }
}