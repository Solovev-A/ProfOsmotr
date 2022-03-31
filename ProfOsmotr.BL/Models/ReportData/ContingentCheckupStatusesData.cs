using ProfOsmotr.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProfOsmotr.BL.Models.ReportData
{
    public class ContingentCheckupStatusesData
    {
        public ContingentCheckupStatusesData(IEnumerable<ContingentCheckupStatus> checkupStatuses)
        {
            var chronicSomaticlDiseases = new List<NewlyDiagnosedDiseasesData>();
            var occupationalDiseases = new List<NewlyDiagnosedDiseasesData>();

            foreach (var status in checkupStatuses)
            {
                ProcessNumbersData(Undergoing, status);
                if (IsCompleted(status))
                {
                    ProcessNumbersData(Completed, status);
                }
                if (IsNotCompleted(status))
                {
                    ProcessNumbersData(NotCompleted, status);
                }
                if (IsNotStarted(status))
                {
                    ProcessNumbersData(NotStarted, status);
                }

                if (!IsCompleted(status)) continue;
                // информация ниже собирается только по завершившим медосмотр работникам

                if (status.CheckupResultId.HasValue && status.CheckupResultId == CheckupResultId.NoContraindications)
                {
                    HasNoContraindications.Increment();
                }
                if (status.CheckupResultId.HasValue &&
                    (status.CheckupResultId == CheckupResultId.TemporaryContraindications
                    || status.CheckupResultId == CheckupResultId.PermanentContraindications))
                {
                    HasNoContraindications.Increment();
                }
                if (status.CheckupResultId.HasValue && status.CheckupResultId == CheckupResultId.NeedAdditionalMedicalExamination)
                {
                    NeedAdditionalMedicalExamination.Increment();
                }
                if (status.NeedExaminationAtOccupationalPathologyCenter)
                {
                    NeedExaminationAtOccupationalPathologyCenter.Increment();
                }
                if (status.NeedOutpatientExamunationAndTreatment)
                {
                    NeedOutpatientExamunationAndTreatment.Increment();
                }
                if (status.NeedInpatientExamunationAndTreatment)
                {
                    NeedInpatientExamunationAndTreatment.Increment();
                }
                if (status.NeedSpaTreatment)
                {
                    NeedSpaTreatment.Increment();
                }
                if (status.NeedDispensaryObservation)
                {
                    NeedDispensaryObservation.Increment();
                }

                foreach (var disease in status.NewlyDiagnosedChronicSomaticDiseases)
                {
                    ProcessNewlyDiagnosedDiseasesLists(chronicSomaticlDiseases, disease);
                }
                foreach (var disease in status.NewlyDiagnosedOccupationalDiseases)
                {
                    ProcessNewlyDiagnosedDiseasesLists(occupationalDiseases, disease);
                }
            }

            NewlyDiagnosedChronicSomaticlDiseases = SortAndNumber(chronicSomaticlDiseases);
            NewlyDiagnosedOccupationalDiseases = SortAndNumber(occupationalDiseases);

            CoveragePercent.Total = new DoubleField(GetCoverage(Completed.Total.IntValue, Undergoing.Total.IntValue));
            CoveragePercent.Women = new DoubleField(GetCoverage(Completed.Women.IntValue, Undergoing.Women.IntValue));
            CoveragePercent.Under18 = new DoubleField(GetCoverage(Completed.Under18.IntValue, Undergoing.Under18.IntValue));
            CoveragePercent.PersistentlyDisabled = new DoubleField(GetCoverage(Completed.PersistentlyDisabled.IntValue, Undergoing.PersistentlyDisabled.IntValue));
        }

        public EmployeesNumbersDataFull<IntField> Undergoing { get; set; } = new EmployeesNumbersDataFull<IntField>();

        public EmployeesNumbersDataFull<IntField> Completed { get; set; } = new EmployeesNumbersDataFull<IntField>();

        public EmployeesNumbersDataFull<DoubleField> CoveragePercent { get; set; } = new EmployeesNumbersDataFull<DoubleField>();

        public EmployeesNumbersDataBase<IntField> NotCompleted { get; set; } = new EmployeesNumbersDataBase<IntField>();

        public EmployeesNumbersDataBase<IntField> NotStarted { get; set; } = new EmployeesNumbersDataBase<IntField>();

        public IntField HasNoContraindications { get; set; } = new IntField();

        public IntField HasContraindications { get; set; } = new IntField();

        public IntField NeedAdditionalMedicalExamination { get; set; } = new IntField();

        public IntField NeedExaminationAtOccupationalPathologyCenter { get; set; } = new IntField();

        public IntField NeedOutpatientExamunationAndTreatment { get; set; } = new IntField();

        public IntField NeedInpatientExamunationAndTreatment { get; set; } = new IntField();

        public IntField NeedSpaTreatment { get; set; } = new IntField();

        public IntField NeedDispensaryObservation { get; set; } = new IntField();

        public IEnumerable<NewlyDiagnosedDiseasesData> NewlyDiagnosedChronicSomaticlDiseases { get; set; }

        public IEnumerable<NewlyDiagnosedDiseasesData> NewlyDiagnosedOccupationalDiseases { get; set; }

        protected bool IsCompleted(ContingentCheckupStatus status)
        {
            CheckupResultId? result = status.CheckupResult?.Id;
            return result.HasValue && result != CheckupResultId.NeedAdditionalMedicalExamination;
        }

        protected bool IsNotCompleted(ContingentCheckupStatus status)
        {
            return !IsNotStarted(status) && !IsCompleted(status);
        }

        protected bool IsNotStarted(ContingentCheckupStatus status)
        {
            return !status.CheckupStarted;
        }

        protected bool IsForWoman(ContingentCheckupStatus status)
        {
            return status.Patient.GenderId == GenderId.Female;
        }

        protected bool IsForUnder18(ContingentCheckupStatus status)
        {
            var reportDate = status.DateOfCompletion.HasValue ? status.DateOfCompletion.Value : DateTime.Now;
            int report = int.Parse(reportDate.ToString("yyyyMMdd"));
            int birth = int.Parse(status.Patient.DateOfBirth.ToString("yyyyMMdd"));
            int age = (report - birth) / 10000;
            return age < 18;
        }

        private void ProcessNumbersData(EmployeesNumbersDataBase<IntField> data, ContingentCheckupStatus status)
        {
            data.Total.Increment();
            if (IsForWoman(status))
            {
                data.Women.Increment();
            }

            if (data is EmployeesNumbersDataFull<IntField> fullData)
            {
                if (IsForUnder18(status))
                {
                    fullData.Under18.Increment();
                }

                if (status.IsDisabled)
                {
                    fullData.PersistentlyDisabled.Increment();
                }
            }
        }

        private double GetCoverage(int completed, int undergoing)
        {
            if (undergoing == 0) return 0;

            return (double)completed * 100 / undergoing;
        }

        private void ProcessNewlyDiagnosedDiseasesLists(List<NewlyDiagnosedDiseasesData> list, NewlyDiagnosedDisease disease)
        {
            var diseaseData = list.FirstOrDefault(d => d.Block == disease.ICD10Chapter.Block);

            if (diseaseData is null)
            {
                diseaseData = new NewlyDiagnosedDiseasesData(disease);
                list.Add(diseaseData);
            }
            else
            {
                diseaseData.Count.Increment();
            }
        }

        private List<NewlyDiagnosedDiseasesData> SortAndNumber(List<NewlyDiagnosedDiseasesData> list)
        {
            if (list.Count == 0)
            {
                return new List<NewlyDiagnosedDiseasesData>
                {
                    NewlyDiagnosedDiseasesData.Empty()
                };
            }

            var sortedList = list.OrderBy(d => d.Block).ToList();
            for (int i = 0; i < sortedList.Count; i++)
            {
                sortedList[i].N = $"{i + 1}.";
            }
            return sortedList;
        }
    }
}