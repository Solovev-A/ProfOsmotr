using ProfOsmotr.BL.Models;

namespace ProfOsmotr.BL
{
    public class ProfessionSearchResultResponse : BaseResponse<ProfessionSearchResult>
    {
        public ProfessionSearchResultResponse(ProfessionSearchResult entity) : base(entity)
        {
        }

        public ProfessionSearchResultResponse(string errorMessage) : base(errorMessage)
        {
        }
    }
}