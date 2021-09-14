using ProfOsmotr.BL.Models;

namespace ProfOsmotr.BL
{
    public class PatientSearchResultResponse : BaseResponse<PatientSearchResult>
    {
        public PatientSearchResultResponse(PatientSearchResult entity) : base(entity)
        {
        }

        public PatientSearchResultResponse(string errorMessage) : base(errorMessage)
        {
        }
    }
}