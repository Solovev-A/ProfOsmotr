namespace ProfOsmotr.DAL
{
    public class ProfessionOrderItem
    {
        public int ProfessionId { get; set; }

        public virtual Profession Profession { get; set; }

        public int OrderItemId { get; set; }

        public virtual OrderItem OrderItem { get; set; }
    }
}