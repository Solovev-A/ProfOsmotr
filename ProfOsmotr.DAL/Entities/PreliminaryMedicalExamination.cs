namespace ProfOsmotr.DAL
{
    public class PreliminaryMedicalExamination : MedicalExamination
    {
        public virtual IndividualCheckupStatus CheckupStatus { get; set; }
    }
}