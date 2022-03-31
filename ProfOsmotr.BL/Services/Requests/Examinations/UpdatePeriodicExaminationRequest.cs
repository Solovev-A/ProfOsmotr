namespace ProfOsmotr.BL
{
    public class UpdatePeriodicExaminationRequest
    {
        public int ExaminationId { get; set; }

        public int EditorId { get; set; }

        public PatchPeriodicExaminationQuery Query { get; set; }
    }
}