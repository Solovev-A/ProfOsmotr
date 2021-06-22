using ProfOsmotr.DAL;

namespace ProfOsmotr.BL
{
    public class EmployerResponse : BaseResponse<Employer>
    {
        public EmployerResponse(Employer entity) : base(entity)
        {
        }

        public EmployerResponse(string errorMessage) : base(errorMessage)
        {
        }
    }
}