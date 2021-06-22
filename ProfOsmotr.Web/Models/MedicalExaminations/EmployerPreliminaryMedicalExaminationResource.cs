namespace ProfOsmotr.Web.Models
{
    public class EmployerPreliminaryMedicalExaminationResource
    {
        public int CheckupStatusId { get; set; }

        public string Patient { get; set; }

        public bool IsCompleted { get; set; }

        public string ReportDate { get; set; }
    }
}
