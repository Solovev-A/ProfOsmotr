using ProfOsmotr.BL.Abstractions;
using ProfOsmotr.BL.Infrastructure;
using ProfOsmotr.BL.Models;
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
        protected readonly TemplateEngineHelper templateEngineHelper;

        public DocxReportsCreator()
        {
            reportsDirectoryPath = GetFullPath(@"ReportsTemp\");
            Directory.CreateDirectory(reportsDirectoryPath);
            checkupStatusMedicalReportTemplatePath = GetFullPath(@"Templates\checkup-status-medical-report.docx");
            templateEngineHelper = new TemplateEngineHelper(reportsDirectoryPath);
        }

        public async Task<BaseFileResult> CreateCheckupStatusMedicalReport(CheckupStatusMedicalReportData data)
        {
            string reportPath = templateEngineHelper.CreateReport(checkupStatusMedicalReportTemplatePath, data);
            string fileName = $"{data.FullName} - мед. заключение";
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