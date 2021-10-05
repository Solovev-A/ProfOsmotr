using ProfOsmotr.DAL;
using System.Collections.Generic;
using System.Linq;

namespace ProfOsmotr.BL.Models.ReportData
{
    public class CheckupStatusExcerptData : CheckupStatusMedicalReportData
    {
        public IEnumerable<CheckupResultsData> CheckupResults { get; set; }

        public string HasCheckupResults { get; set; }

        public CheckupStatusExcerptData(CheckupStatus checkupStatus) : base(checkupStatus)
        {
            if (checkupStatus is IndividualCheckupStatus iStatus)
            {
                CheckupResults = GetData(iStatus.IndividualCheckupIndexValues);
            }
            else if (checkupStatus is ContingentCheckupStatus cStatus)
            {
                CheckupResults = GetData(cStatus.ContingentCheckupIndexValues);
            }

            HasCheckupResults = CheckupResults.Any() ? string.Empty : "прилагаются";


            // TemplateEngine.Docx не обрабатывает пустые списки
            if (!CheckupResults.Any())
            {
                CheckupResults = new[]
                {
                    new CheckupResultsData()
                    {
                        Indexes = new []
                        {
                            new ExaminationResultIndexData()
                        }
                    }
                };
            }

            IEnumerable<CheckupResultsData> GetData(IEnumerable<CheckupIndexValue> indexValues)
            {
                return indexValues.GroupBy(iv => iv.ExaminationResultIndex.OrderExamination.Name)
                    .Select(g => new CheckupResultsData()
                    {
                        ExaminationName = g.Key,
                        Indexes = g.Select(iv => new ExaminationResultIndexData()
                        {
                            Name = iv.ExaminationResultIndex.Title,
                            UnitOfMeasure = iv.ExaminationResultIndex.UnitOfMeasure,
                            Value = iv.Value
                        })
                    });
            }
        }
    }
}