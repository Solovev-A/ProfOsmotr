namespace ProfOsmotr.BL
{
    public class CreateEmployerRequest
    {
        public int ClinicId { get; set; }

        public string Name { get; set; }

        public string HeadLastName { get; set; }

        public string HeadFirstName { get; set; }

        public string HeadPatronymicName { get; set; }

        public string HeadPosition { get; set; }
    }
}