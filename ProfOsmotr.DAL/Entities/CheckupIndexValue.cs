namespace ProfOsmotr.DAL
{
    /// <summary>
    /// Представляет значение показателя медицинского обследования
    /// </summary>
    public class CheckupIndexValue
    {
        public int ExaminationResultIndexId { get; set; }

        public virtual ExaminationResultIndex ExaminationResultIndex { get; set; }

        public string Value { get; set; }
    }

    public class IndividualCheckupIndexValue : CheckupIndexValue
    {
        public int IndividualCheckupStatusId { get; set; }

        public virtual IndividualCheckupStatus IndividualCheckupStatus { get; set; }
    }

    public class ContingentCheckupIndexValue : CheckupIndexValue
    {
        public int ContingentCheckupStatusId { get; set; }

        public virtual ContingentCheckupStatus ContingentCheckupStatus { get; set; }
    }
}