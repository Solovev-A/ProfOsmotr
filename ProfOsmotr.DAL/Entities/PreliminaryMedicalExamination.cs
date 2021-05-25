namespace ProfOsmotr.DAL
{
    public class PreliminaryMedicalExamination : MedicalExamination
    {
        public int CheckupStatusId { get; set; }

        public virtual IndividualCheckupStatus CheckupStatus { get; set; }
    }
}