using ProfOsmotr.DAL;
using System.Collections.Generic;
using System.Linq;

namespace ProfOsmotr.BL.Models.ReportData
{
    public class PeriodicExaminationReportData : ContingentCheckupStatusesData
    {
        public PeriodicExaminationReportData(PeriodicMedicalExamination examination) : base(examination.Statuses)
        {
            var employer = examination.Employer;

            Clinic = new ClinicReportData(examination.Clinic);
            ReportDate = examination.ReportDate.HasValue ? examination.ReportDate.Value.ToString("dd.MM.yyyy") : string.Empty;
            Employer = examination.Employer.Name;
            ExaminationYear = examination.ExaminationYear.ToString();
            EmployerHeadName = $"{employer.HeadLastName} {employer.HeadFirstName[0]}. {employer.HeadPatronymicName[0]}.";
            EmployerHeadPosition = employer.HeadPosition ?? string.Empty;
            Recommendations = examination.Recommendations ?? string.Empty;

            SetCheckupStatuses(examination);

            var employerData = examination.EmployerData;
            Employees = new EmployeesNumbersDataFull<IntField>
            {
                Total = new IntField(employerData.EmployeesTotal),
                Women = new IntField(employerData.EmployeesWomen),
                Under18 = new IntField(employerData.EmployeesUnder18),
                PersistentlyDisabled = new IntField(employerData.EmployeesPersistentlyDisabled)
            };
            WorkingWithHarmfulFactors = new EmployeesNumbersDataFull<IntField>
            {
                Total = new IntField(employerData.WorkingWithHarmfulFactorsTotal),
                Women = new IntField(employerData.WorkingWithHarmfulFactorsWomen),
                Under18 = new IntField(employerData.WorkingWithHarmfulFactorsUnder18),
                PersistentlyDisabled = new IntField(employerData.WorkingWithHarmfulFactorsPersistentlyDisabled)
            };
            WorkingWithJobTypes = new EmployeesNumbersDataFull<IntField>
            {
                Total = new IntField(employerData.WorkingWithJobTypesTotal),
                Women = new IntField(employerData.WorkingWithJobTypesWomen),
                Under18 = new IntField(employerData.WorkingWithJobTypesUnder18),
                PersistentlyDisabled = new IntField(employerData.WorkingWithJobTypesPersistentlyDisabled)
            };
        }

        public ClinicReportData Clinic { get; set; }

        public string ReportDate { get; set; }

        public string Employer { get; set; }

        public string ExaminationYear { get; set; }

        public string EmployerHeadName { get; set; }

        public string EmployerHeadPosition { get; set; }

        public IEnumerable<CheckupStatusDataListItem> CompletedList { get; set; }

        public IEnumerable<PatientBaseListItem> NotCompletedList { get; set; }

        public IEnumerable<PatientBaseListItem> NotStartedList { get; set; }

        public IEnumerable<OccupationalDiseasesCheckupStatusListItem> OccupationalDiseasesCheckupStatuses { get; set; }

        public string Recommendations { get; set; }

        public EmployeesNumbersDataFull<IntField> Employees { get; set; }

        public EmployeesNumbersDataBase<IntField> WorkingWithHarmfulFactors { get; set; }

        public EmployeesNumbersDataFull<IntField> WorkingWithJobTypes { get; set; }

        private void SetCheckupStatuses(PeriodicMedicalExamination examination)
        {
            var completed = new List<CheckupStatusDataListItem>();
            var notCompleted = new List<PatientBaseListItem>();
            var notStarted = new List<PatientBaseListItem>();
            var occupationalDiseasesStatuses = new List<OccupationalDiseasesCheckupStatusListItem>();

            foreach (var status in examination.Statuses)
            {
                if (IsCompleted(status))
                {
                    var newStatus = new CheckupStatusDataListItem(status, (completed.Count + 1).ToString());
                    if (newStatus.EmployerDepartment == string.Empty)
                    {
                        newStatus.EmployerDepartment = status.Profession?.Name ?? string.Empty;
                    }
                    completed.Add(newStatus);
                }
                else if (IsNotCompleted(status))
                {
                    notCompleted.Add(new PatientBaseListItem(status.Patient, (notCompleted.Count + 1).ToString()));
                }
                else if (IsNotStarted(status))
                {
                    notStarted.Add(new PatientBaseListItem(status.Patient, (notStarted.Count + 1).ToString()));
                }

                if (status.NewlyDiagnosedOccupationalDiseases.Any())
                {
                    occupationalDiseasesStatuses.Add(new OccupationalDiseasesCheckupStatusListItem(
                        status,
                        (occupationalDiseasesStatuses.Count + 1).ToString()));
                }
            }

            if (completed.Count == 0)
            {
                completed.Add(CheckupStatusDataListItem.Empty());
            }
            if (notCompleted.Count == 0)
            {
                notCompleted.Add(PatientBaseListItem.Empty());
            }
            if (notStarted.Count == 0)
            {
                notStarted.Add(PatientBaseListItem.Empty());
            }
            if (occupationalDiseasesStatuses.Count == 0)
            {
                occupationalDiseasesStatuses.Add(OccupationalDiseasesCheckupStatusListItem.Empty());
            }

            CompletedList = completed;
            NotCompletedList = notCompleted;
            NotStartedList = notStarted;
            OccupationalDiseasesCheckupStatuses = occupationalDiseasesStatuses;
        }
    }
}