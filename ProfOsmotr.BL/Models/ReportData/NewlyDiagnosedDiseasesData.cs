namespace ProfOsmotr.BL.Models.ReportData
{
    public class NewlyDiagnosedDiseasesData
    {
        public NewlyDiagnosedDiseasesData(DAL.NewlyDiagnosedDisease newlyDiagnosedDisease)
        {
            Block = newlyDiagnosedDisease.ICD10Chapter.Block;
            Count = new IntField(newlyDiagnosedDisease.Cases);
        }

        protected NewlyDiagnosedDiseasesData()
        {
        }

        public string N { get; set; }

        public string Block { get; set; }

        public IntField Count { get; set; }

        public static NewlyDiagnosedDiseasesData Empty()
        {
            var emptyValue = "-";
            return new NewlyDiagnosedDiseasesData
            {
                N = emptyValue,
                Block = emptyValue
            };
        }
    }
}