using ProfOsmotr.BL.Abstractions;

namespace ProfOsmotr.BL.Models.ReportData
{
    public class EmployeesNumbersDataFull<TField> : EmployeesNumbersDataBase<TField>
        where TField : IReportField, new()
    {
        public EmployeesNumbersDataFull()
        {
            Under18 = new TField();
            PersistentlyDisabled = new TField();
        }

        public TField Under18 { get; set; }

        public TField PersistentlyDisabled { get; set; }
    }
}