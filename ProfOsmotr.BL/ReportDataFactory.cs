using ProfOsmotr.BL.Abstractions;
using ProfOsmotr.BL.Models;
using ProfOsmotr.DAL;
using System.Linq;

namespace ProfOsmotr.BL
{
    public class ReportDataFactory : IReportDataFactory
    {
        protected const string DATE_FORMAT = "dd.MM.yyyy";

        public CheckupStatusMedicalReportData CreateCheckupStatusMedicalReportData(CheckupStatus checkupStatus)
        {
            ClinicReportData clinicData = null;
            string examinationType = string.Empty;
            string employer = string.Empty;

            if (checkupStatus is IndividualCheckupStatus iStatus)
            {
                clinicData = CreateClinicReportData(iStatus.PreliminaryMedicalExamination.Clinic);
                examinationType = "предварительного";
                employer = iStatus.PreliminaryMedicalExamination.Employer?.Name ?? string.Empty;
            }
            else if (checkupStatus is ContingentCheckupStatus cStatus)
            {
                clinicData = CreateClinicReportData(cStatus.PeriodicMedicalExamination.Clinic);
                examinationType = "периодического";
                employer = cStatus.PeriodicMedicalExamination.Employer?.Name ?? string.Empty;
            }

            return new CheckupStatusMedicalReportData()
            {
                CheckupResult = checkupStatus.CheckupResult?.Text ?? string.Empty,
                Clinic = clinicData,
                DateOfBirth = checkupStatus.Patient.DateOfBirth.ToString(DATE_FORMAT),
                DateOfCompletion = checkupStatus.DateOfCompletion.HasValue
                    ? checkupStatus.DateOfCompletion.Value.ToString(DATE_FORMAT)
                    : string.Empty,
                Employer = employer,
                EmployerDepartment = checkupStatus.EmployerDepartment?.Name ?? string.Empty,
                ExaminationType = examinationType,
                FullName = GetPatientFullName(checkupStatus.Patient),
                Gender = GetGenderShortName(checkupStatus.Patient.GenderId),
                MedicalReport = checkupStatus.MedicalReport ?? string.Empty,
                OrderItems = GetOrderItemsString(checkupStatus.Profession),
                Profession = checkupStatus.Profession?.Name ?? string.Empty
            };
        }

        public ClinicReportData CreateClinicReportData(Clinic clinic)
        {
            return new ClinicReportData()
            {
                Address = clinic.ClinicDetails.Address,
                Name = clinic.ClinicDetails.FullName,
                Phone = clinic.ClinicDetails.Phone
            };
        }

        protected string GetPatientFullName(Patient patient)
        {
            return $"{patient.LastName} {patient.FirstName} {patient.PatronymicName}".Trim();
        }

        protected string GetGenderShortName(GenderId genderId)
        {
            return genderId == GenderId.Male ? "М" : "Ж";
        }

        protected string GetOrderItemsString(Profession profession)
        {
            if (profession is null) return string.Empty;
            if (!profession.OrderItems.Any()) return "отсутствуют";

            var keys = profession.OrderItems.Select(i => i.Key);

            return string.Join("; ", keys);
        }
    }
}