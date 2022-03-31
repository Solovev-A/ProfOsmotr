using ProfOsmotr.BL.Models;

namespace ProfOsmotr.BL
{
    public class FileResultResponse : BaseResponse<BaseFileResult>
    {
        public FileResultResponse(BaseFileResult entity) : base(entity)
        {
        }

        public FileResultResponse(string errorMessage) : base(errorMessage)
        {
        }
    }
}