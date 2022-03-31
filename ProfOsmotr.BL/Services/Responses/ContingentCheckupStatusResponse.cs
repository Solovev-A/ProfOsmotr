using ProfOsmotr.DAL;

namespace ProfOsmotr.BL
{
    public class ContingentCheckupStatusResponse : BaseResponse<ContingentCheckupStatus>
    {
        public ContingentCheckupStatusResponse(ContingentCheckupStatus entity) : base(entity)
        {
        }

        public ContingentCheckupStatusResponse(string errorMessage) : base(errorMessage)
        {
        }
    }
}