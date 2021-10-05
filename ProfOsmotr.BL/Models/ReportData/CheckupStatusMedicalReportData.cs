using ProfOsmotr.DAL;
using System.Linq;

namespace ProfOsmotr.BL.Models.ReportData
{
    public class CheckupStatusMedicalReportData : CheckupStatusData
    {
        public ClinicReportData Clinic { get; set; }

        public string ExaminationType { get; set; }

        public string Employer { get; set; }

        public string Profession { get; set; }

        public string OrderItems { get; set; }

        public string DateOfCompletion { get; set; }

        public CheckupStatusMedicalReportData(CheckupStatus checkupStatus) : base(checkupStatus)
        {
            if (checkupStatus is IndividualCheckupStatus iStatus)
            {
                Clinic = new ClinicReportData(iStatus.PreliminaryMedicalExamination.Clinic);
                ExaminationType = "предварительного";
                Employer = iStatus.PreliminaryMedicalExamination.Employer?.Name ?? string.Empty;
            }
            else if (checkupStatus is ContingentCheckupStatus cStatus)
            {
                Clinic = new ClinicReportData(cStatus.PeriodicMedicalExamination.Clinic);
                ExaminationType = "периодического";
                Employer = cStatus.PeriodicMedicalExamination.Employer?.Name ?? string.Empty;
            }

            DateOfCompletion = checkupStatus.DateOfCompletion.HasValue
                ? checkupStatus.DateOfCompletion.Value.ToString(DATE_FORMAT)
                : string.Empty;
            OrderItems = GetOrderItemsString(checkupStatus.Profession);
            Profession = checkupStatus.Profession?.Name ?? string.Empty;


            string GetOrderItemsString(Profession profession)
            {
                if (profession is null) return string.Empty;
                if (!profession.OrderItems.Any()) return "отсутствуют";

                var keys = profession.OrderItems.Select(i => i.Key);

                return string.Join("; ", keys);
            }
        }
    }
}