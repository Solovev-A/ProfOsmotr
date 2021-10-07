using ProfOsmotr.DAL;
using System;

namespace ProfOsmotr.BL.Models.ReportData
{
    public class CheckupStatusDataBase : PatientBase
    {
        protected const string DATE_FORMAT = "dd.MM.yyyy";

        public CheckupStatusDataBase(CheckupStatus checkupStatus) : base(checkupStatus.Patient)
        {
            if (checkupStatus is null) throw new ArgumentNullException(nameof(checkupStatus));

            DateOfBirth = checkupStatus.Patient.DateOfBirth.ToString(DATE_FORMAT);
            EmployerDepartment = checkupStatus.EmployerDepartment?.Name ?? string.Empty;
            Gender = checkupStatus.Patient.GenderId == GenderId.Male ? "М" : "Ж";
        }

        protected CheckupStatusDataBase()
        {
        }

        public string Gender { get; set; }

        public string DateOfBirth { get; set; }

        public string EmployerDepartment { get; set; }
    }
}