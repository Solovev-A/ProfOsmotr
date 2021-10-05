using ProfOsmotr.BL.Abstractions;
using ProfOsmotr.BL.Infrastructure;
using ProfOsmotr.BL.Models;
using ProfOsmotr.BL.Models.ReportData;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace ProfOsmotr.BL
{
    public class DocxReportsCreator : IReportsCreator
    {
        protected readonly string reportsDirectoryPath;
        protected readonly string checkupStatusMedicalReportTemplatePath;
        protected readonly string checkupStatusExcerptTemplatePath;
        protected readonly TemplateEngineHelper templateEngineHelper;

        public DocxReportsCreator()
        {
            reportsDirectoryPath = GetFullPath(@"ReportsTemp\");
            Directory.CreateDirectory(reportsDirectoryPath);
            checkupStatusMedicalReportTemplatePath = GetFullPath(@"Templates\checkup-status-medical-report.docx");
            checkupStatusExcerptTemplatePath = GetFullPath(@"Templates\checkup-status-excerpt.docx");
            templateEngineHelper = new TemplateEngineHelper(reportsDirectoryPath);
        }

        public async Task<BaseFileResult> CreateCheckupStatusesMedicalReport(params CheckupStatusMedicalReportData[] datas)
        {
            return await CreateCheckupStatusesReport(datas, checkupStatusMedicalReportTemplatePath, "заключение");
        }

        public async Task<BaseFileResult> CreateCheckupStatusExcerpt(params CheckupStatusExcerptData[] datas)
        {
            return await CreateCheckupStatusesReport(datas, checkupStatusExcerptTemplatePath, "выписка");
        }

        protected async Task<BaseFileResult> CreateCheckupStatusesReport<TData>(TData[] datas,
                                                                              string templatePath,
                                                                              string fileNameLabel)
            where TData : CheckupStatusMedicalReportData
        {
            if (datas is null || datas.Length == 0)
                throw new ArgumentException();

            var data = new MultiItemReportData<TData>(datas);
            string reportPath = templateEngineHelper.CreateReport(templatePath, data);
            string fileName = datas.Length > 1
                ? $"{datas[0].Employer} - {fileNameLabel}"
                : $"{datas[0].FullName} - {fileNameLabel}";
            return await CreateResult(reportPath, fileName);
        }

        protected async Task<BaseFileResult> CreateResult(string filePath, string resultFileName)
        {
            byte[] bytes = await File.ReadAllBytesAsync(filePath);
            var result = new DocxFileResult(bytes, resultFileName);
            File.Delete(filePath);
            return result;
        }

        protected string GetFullPath(string filePath)
        {
            string executionDirectoryPathName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\";
            return Path.Combine(executionDirectoryPathName, filePath);
        }
    }
}