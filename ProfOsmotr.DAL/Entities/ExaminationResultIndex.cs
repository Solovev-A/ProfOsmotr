namespace ProfOsmotr.DAL
{
    public class ExaminationResultIndex
    {
        public int Id { get; set; }

        public int OrderExaminationId { get; set; }

        public virtual OrderExamination OrderExamination { get; set; }

        public string Title { get; set; }

        public string UnitOfMeasure { get; set; }

        public bool IsDeleted { get; set; }
    }
}