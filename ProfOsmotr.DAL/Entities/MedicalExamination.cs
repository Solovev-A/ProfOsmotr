namespace ProfOsmotr.DAL
{
    public abstract class MedicalExamination
    {
        public int Id { get; set; }

        public int ClinicId { get; set; }

        public virtual Clinic Clinic { get; set; }

        public int? EmployerId { get; set; }

        public virtual Employer Employer { get; set; }

        public bool Completed { get; set; }

        public string Recommendations { get; set; }

        public int LastEditorId { get; set; }

        public virtual User LastEditor { get; set; }
    }
}