namespace ProfOsmotr.DAL
{
    public class IndividualMedicalExamination : MedicalExamination
    {
        public int CheckupStatusId { get; set; }

        public virtual IndividualCheckupStatus CheckupStatus { get; set; }
    }
}