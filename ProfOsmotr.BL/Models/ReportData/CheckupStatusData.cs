using ProfOsmotr.DAL;
using System;

namespace ProfOsmotr.BL.Models.ReportData
{
    public class CheckupStatusData : PatientBase
    {
        protected const string DATE_FORMAT = "dd.MM.yyyy";

        public string Gender { get; set; }

        public string DateOfBirth { get; set; }

        public string EmployerDepartment { get; set; }

        public string CheckupResult { get; set; }

        public string MedicalReport { get; set; }

        public CheckupStatusData(CheckupStatus checkupStatus) : base(checkupStatus.Patient)
        {
            if (checkupStatus is null) throw new ArgumentNullException(nameof(checkupStatus));

            CheckupResult = checkupStatus.CheckupResult?.Text ?? string.Empty;
            DateOfBirth = checkupStatus.Patient.DateOfBirth.ToString(DATE_FORMAT);
            EmployerDepartment = checkupStatus.EmployerDepartment?.Name ?? string.Empty;
            Gender = checkupStatus.Patient.GenderId == GenderId.Male ? "М" : "Ж";
            MedicalReport = checkupStatus.MedicalReport ?? string.Empty;
        }
    }
}