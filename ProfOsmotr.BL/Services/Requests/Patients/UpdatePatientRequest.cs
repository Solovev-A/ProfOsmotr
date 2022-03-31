namespace ProfOsmotr.BL
{
    public class UpdatePatientRequest
    {
        public int PatientId { get; set; }

        public PatchPatientQuery Query { get; set; }
    }
}