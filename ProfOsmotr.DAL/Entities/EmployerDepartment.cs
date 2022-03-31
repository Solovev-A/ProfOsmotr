namespace ProfOsmotr.DAL
{
    public class EmployerDepartment
    {
        public int Id { get; private set; }

        public string Name { get; set; }

        public int ParentId { get; set; }

        public virtual Employer Parent { get; set; }
    }
}