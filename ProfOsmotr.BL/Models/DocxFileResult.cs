namespace ProfOsmotr.BL.Models
{
    public class DocxFileResult : BaseFileResult
    {
        public DocxFileResult(byte[] bytes, string fileName)
            : base(bytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", $"{fileName}.docx")
        {
        }
    }
}