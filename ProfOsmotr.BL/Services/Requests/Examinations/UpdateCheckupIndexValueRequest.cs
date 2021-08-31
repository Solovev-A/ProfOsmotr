namespace ProfOsmotr.BL
{
    public class UpdateCheckupIndexValueRequest
    {
        /// <summary>
        /// Идентификатор результата обследования
        /// </summary>
        public int Id { get; set; }

        public string Value { get; set; }
    }
}