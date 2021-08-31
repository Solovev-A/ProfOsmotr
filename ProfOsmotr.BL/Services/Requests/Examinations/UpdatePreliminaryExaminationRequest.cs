namespace ProfOsmotr.BL
{
    public class UpdatePreliminaryExaminationRequest
    {
        public int ExaminationId { get; set; }

        public int EditorId { get; set; }

        public PatchPreliminaryExaminationQuery Query { get; set; }
    }
}