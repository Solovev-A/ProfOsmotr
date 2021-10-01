namespace ProfOsmotr.BL.Models
{
    public class CheckupStatusMedicalReportData
    {
        public ClinicReportData Clinic { get; set; }

        public string ExaminationType { get; set; }

        public string FullName { get; set; }

        public string Gender { get; set; }

        public string DateOfBirth { get; set; }

        public string Employer { get; set; }

        public string EmployerDepartment { get; set; }

        public string Profession { get; set; }

        public string OrderItems { get; set; }

        public string CheckupResult { get; set; }

        public string MedicalReport { get; set; }

        public string DateOfCompletion { get; set; }
    }
}