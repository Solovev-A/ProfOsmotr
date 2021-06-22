namespace ProfOsmotr.BL
{
    public class UpdateEmployerRequest
    {
        public int EmployerId { get; set; }

        public PatchEmployerQuery Query { get; set; }
    }
}