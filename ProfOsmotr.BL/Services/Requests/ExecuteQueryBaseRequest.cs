namespace ProfOsmotr.BL
{
    public class ExecuteQueryBaseRequest
    {
        public int ClinicId { get; set; }

        public int Page { get; set; }

        public int ItemsPerPage { get; set; }

        public string Search { get; set; }

        public int Start => (Page - 1) * ItemsPerPage;
    }
}