using System.Collections.Generic;

namespace ProfOsmotr.DAL
{
    public class Employer
    {
        public int Id { get; private set; }

        public string Name { get; set; }

        public string HeadLastName { get; set; }

        public string HeadFirstName { get; set; }

        public string HeadPatronymicName { get; set; }

        public string HeadPosition { get; set; }

        public virtual ICollection<EmployerDepartment> Departments { get; } = new List<EmployerDepartment>();

        public int ClinicId { get; set; }

        public virtual Clinic Clinic { get; set; }
    }
}