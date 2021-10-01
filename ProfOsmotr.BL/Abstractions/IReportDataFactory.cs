using ProfOsmotr.BL.Models;
using ProfOsmotr.DAL;

namespace ProfOsmotr.BL.Abstractions
{
    public interface IReportDataFactory
    {
        CheckupStatusMedicalReportData CreateCheckupStatusMedicalReportData(CheckupStatus checkupStatus);
    }
}