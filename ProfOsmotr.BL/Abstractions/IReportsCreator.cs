using ProfOsmotr.BL.Models;
using ProfOsmotr.BL.Models.ReportData;
using System.Threading.Tasks;

namespace ProfOsmotr.BL.Abstractions
{
    /// <summary>
    /// Представляет абстрацию для генератора отчетов
    /// </summary>
    public interface IReportsCreator
    {
        /// <summary>
        /// Создает медицинские заключения для медосмотров работников
        /// </summary>
        /// <param name="datas">Данные о медосмотрах работников</param>
        /// <returns></returns>
        Task<BaseFileResult> CreateCheckupStatusesMedicalReport(params CheckupStatusMedicalReportData[] datas);

        /// <summary>
        /// Создает выписки по результатам медосмотров работников
        /// </summary>
        /// <param name="datas">Данные о медосмотрах работников</param>
        /// <returns></returns>
        Task<BaseFileResult> CreateCheckupStatusExcerpt(params CheckupStatusExcerptData[] datas);

        /// <summary>
        /// Создает заключительный акт периодического медосмотра
        /// </summary>
        /// <param name="data">Данные о периодическом медосмотре</param>
        /// <returns></returns>
        Task<BaseFileResult> CreatePeriodicMedicalExaminationReport(PeriodicExaminationReportData data);

        /// <summary>
        /// Создает годовой отчет периодических медосмотров
        /// </summary>
        /// <param name="data">Данные годового отчета периодических медосмотров</param>
        /// <returns></returns>
        Task<BaseFileResult> CreatePeriodicMedicalExaminationsYearReport(PeriodicExaminationsYearReportData data);
    }
}