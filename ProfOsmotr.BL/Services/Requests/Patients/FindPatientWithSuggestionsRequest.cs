namespace ProfOsmotr.BL
{
    public class FindPatientWithSuggestionsRequest
    {
        public int ClinicId { get; set; }

        public string Search { get; set; }

        public int EmployerId { get; set; }
    }
}