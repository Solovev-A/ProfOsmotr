using ProfOsmotr.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProfOsmotr.BL.Models.ReportData
{
    public class PeriodicExaminationsYearReportData : ContingentCheckupStatusesData
    {
        public PeriodicExaminationsYearReportData(IEnumerable<PeriodicMedicalExamination> examinations)
            : base(examinations.SelectMany(ex => ex.Statuses))
        {
            if (!examinations.Any()) throw new ArgumentException("Для формирования годового отчета нужен хотя бы один медосмотр");

            Clinic = new ClinicReportData(examinations.First().Clinic);
            ExaminationsYear = examinations.First().ExaminationYear.ToString();
            YearReportDate = DateTime.Today.ToString("dd.MM.yyyy");

            Employees = new EmployeesNumbersDataFull<IntField>();
            WorkingWithHarmfulFactors = new EmployeesNumbersDataFull<IntField>();
            WorkingWithJobTypes = new EmployeesNumbersDataFull<IntField>();

            var employers = new List<EmployerData>();

            foreach (var examination in examinations)
            {
                employers.Add(new EmployerData(examination.Employer));

                var employerData = examination.EmployerData;
                if (employerData is null) continue;

                Employees.Total.Add(employerData.EmployeesTotal);
                Employees.Women.Add(employerData.EmployeesWomen);
                Employees.Under18.Add(employerData.EmployeesUnder18);
                Employees.PersistentlyDisabled.Add(employerData.EmployeesPersistentlyDisabled);

                WorkingWithHarmfulFactors.Total.Add(employerData.WorkingWithHarmfulFactorsTotal);
                WorkingWithHarmfulFactors.Women.Add(employerData.WorkingWithHarmfulFactorsWomen);
                WorkingWithHarmfulFactors.Under18.Add(employerData.WorkingWithHarmfulFactorsUnder18);
                WorkingWithHarmfulFactors.PersistentlyDisabled.Add(employerData.WorkingWithHarmfulFactorsPersistentlyDisabled);

                WorkingWithJobTypes.Total.Add(employerData.WorkingWithJobTypesTotal);
                WorkingWithJobTypes.Women.Add(employerData.WorkingWithJobTypesWomen);
                WorkingWithJobTypes.Under18.Add(employerData.WorkingWithJobTypesUnder18);
                WorkingWithJobTypes.PersistentlyDisabled.Add(employerData.WorkingWithJobTypesPersistentlyDisabled);
            }

            Employers = employers.OrderBy(e => e.Name);
        }

        public ClinicReportData Clinic { get; set; }

        public string ExaminationsYear { get; set; }

        public string YearReportDate { get; set; }

        public IEnumerable<EmployerData> Employers { get; set; }

        public EmployeesNumbersDataFull<IntField> Employees { get; set; }

        public EmployeesNumbersDataFull<IntField> WorkingWithHarmfulFactors { get; set; }

        public EmployeesNumbersDataFull<IntField> WorkingWithJobTypes { get; set; }

        public class EmployerData
        {
            public EmployerData(Employer employer)
            {
                if (employer is null) throw new ArgumentNullException(nameof(employer));

                Name = employer.Name;
            }

            public string Name { get; set; }
        }
    }
}