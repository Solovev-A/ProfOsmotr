using ProfOsmotr.BL.Abstractions;

namespace ProfOsmotr.BL.Models.ReportData
{
    public class EmployeesNumbersDataBase<TField>
        where TField : IReportField, new()
    {
        public EmployeesNumbersDataBase()
        {
            Total = new TField();
            Women = new TField();
        }

        public TField Total { get; set; }

        public TField Women { get; set; }
    }
}