namespace ProfOsmotr.DAL
{
    public class OrderItemOrderExamination
    {
        public int OrderItemId { get; private set; }

        public virtual OrderItem OrderItem { get; set; }

        public int OrderExaminationId { get; set; }

        public virtual OrderExamination OrderExamination { get; set; }
    }
}