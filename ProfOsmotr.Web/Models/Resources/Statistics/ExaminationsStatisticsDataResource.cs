namespace ProfOsmotr.Web.Models
{
    public class ExaminationsStatisticsDataResource
    {
        public string Period { get; set; }

        public int PreliminaryExaminationsCount { get; set; }

        public int ContingentCheckupStatusesCount { get; set; }
    }
}