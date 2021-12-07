namespace ProfOsmotr.Web.Models
{
    public class BasePeriodicExaminationListItemResource
    {
        public int Id { get; set; }

        public int ExaminationYear { get; set; }

        public bool IsCompleted { get; set; }

        public string ReportDate { get; set; }
    }
}