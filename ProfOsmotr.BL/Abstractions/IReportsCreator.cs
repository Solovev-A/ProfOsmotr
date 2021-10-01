using ProfOsmotr.BL.Models;
using System.Threading.Tasks;

namespace ProfOsmotr.BL.Abstractions
{
    public interface IReportsCreator
    {
        Task<BaseFileResult> CreateCheckupStatusMedicalReport(CheckupStatusMedicalReportData data);
    }
}