using ProfOsmotr.DAL;

namespace ProfOsmotr.BL
{
    public class EmployerDepartmentResponse : BaseResponse<EmployerDepartment>
    {
        public EmployerDepartmentResponse(EmployerDepartment entity) : base(entity)
        {
        }

        public EmployerDepartmentResponse(string errorMessage) : base(errorMessage)
        {
        }
    }
}