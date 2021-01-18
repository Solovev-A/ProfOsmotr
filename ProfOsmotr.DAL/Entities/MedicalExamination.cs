namespace ProfOsmotr.DAL
{
    public abstract class MedicalExamination
    {
        public int Id { get; set; }

        public int ClinicId { get; set; }

        public virtual Clinic Clinic { get; set; }

        public int? EmployerId { get; set; }

        public virtual Employer Employer { get; set; }

        public MedicalExaminationTypeId MedicalExaminationTypeId { get; set; }

        public virtual MedicalExaminationType MedicalExaminationType { get; set; }

        public bool Completed { get; set; }

        public int? EmployerDataId { get; set; }

        public virtual EmployerData EmployerData { get; set; }

        public string Recommendations { get; set; }
    }
}