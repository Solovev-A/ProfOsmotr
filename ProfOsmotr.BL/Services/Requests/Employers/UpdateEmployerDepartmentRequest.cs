namespace ProfOsmotr.BL
{
    public class UpdateEmployerDepartmentRequest
    {
        public int EmployerDepartmentId { get; set; }

        public PatchEmployerDepartmentQuery Query { get; set; }
    }
}