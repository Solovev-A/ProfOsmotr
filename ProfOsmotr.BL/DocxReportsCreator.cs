using ProfOsmotr.BL.Abstractions;
using ProfOsmotr.BL.Infrastructure;
using ProfOsmotr.BL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public async Task<BaseFileResult> CreateCheckupStatusesMedicalReport(params CheckupStatusMedicalReportData[] datas)
        {
            if (datas is null || datas.Length == 0)
                throw new ArgumentException();

            var data = new { Reports = (IEnumerable<CheckupStatusMedicalReportData>)datas };
            string reportPath = templateEngineHelper.CreateReport(checkupStatusMedicalReportTemplatePath, data);
            string fileName = datas.Length > 1
                ? $"{datas[0].Employer} - заключения"
                : $"{datas[0].FullName} - заключение";
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