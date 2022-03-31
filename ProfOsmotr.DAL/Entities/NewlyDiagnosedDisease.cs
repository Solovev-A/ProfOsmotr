namespace ProfOsmotr.DAL
{
    public abstract class NewlyDiagnosedDisease
    {
        public int ContingentCheckupStatusId { get; set; }

        public virtual ContingentCheckupStatus ContingentCheckupStatus { get; set; }

        public int ICD10ChapterId { get; set; }

        public virtual ICD10Chapter ICD10Chapter { get; set; }

        public int Cases { get; set; }
    }

    public class NewlyDiagnosedChronicSomaticDisease : NewlyDiagnosedDisease
    {
    }

    public class NewlyDiagnosedOccupationalDisease : NewlyDiagnosedDisease
    {
    }
}