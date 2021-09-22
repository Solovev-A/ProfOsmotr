namespace ProfOsmotr.BL
{
    public class UpdateContingentCheckupStatusRequest
    {
        public int CheckupStatusId { get; set; }

        public int EditorId { get; set; }

        public PatchContingentCheckupStatusQuery Query { get; set; }
    }
}