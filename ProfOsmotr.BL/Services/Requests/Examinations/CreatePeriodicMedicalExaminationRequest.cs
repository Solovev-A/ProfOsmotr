namespace ProfOsmotr.BL
{
    public class CreatePeriodicMedicalExaminationRequest
    {
        public int EmployerId { get; set; }

        public int ExaminationYear { get; set; }

        public int CreatorId { get; set; }
    }
}