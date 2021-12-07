namespace ProfOsmotr.Web.Models
{
    public class CalculationResultItemResource
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string ExaminationName { get; set; }

        public string FullName { get; set; }

        public decimal Price { get; set; }

        public int Amount { get; set; }

        public int ServiceAvailabilityGroupId { get; set; }

        public decimal Sum => Price * Amount;
    }
}