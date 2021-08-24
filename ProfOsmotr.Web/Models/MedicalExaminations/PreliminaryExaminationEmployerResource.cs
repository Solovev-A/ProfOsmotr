namespace ProfOsmotr.Web.Models
{
    public class PreliminaryExaminationEmployerResource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public EmployerDepartmentResource Department { get; set; }
    }
}