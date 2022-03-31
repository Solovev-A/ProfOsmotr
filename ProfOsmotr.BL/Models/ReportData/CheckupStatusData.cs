using ProfOsmotr.DAL;

namespace ProfOsmotr.BL.Models.ReportData
{
    public class CheckupStatusData : CheckupStatusDataBase
    {
        public string CheckupResult { get; set; }

        public string MedicalReport { get; set; }

        public CheckupStatusData(CheckupStatus checkupStatus) : base(checkupStatus)
        {
            CheckupResult = checkupStatus.CheckupResult?.Text ?? string.Empty;
            MedicalReport = checkupStatus.MedicalReport ?? string.Empty;
        }

        protected CheckupStatusData()
        {
        }
    }
}