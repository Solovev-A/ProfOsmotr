using ProfOsmotr.DAL;
using System.Collections.Generic;

namespace ProfOsmotr.BL
{
    public class ICD10ChaptersResponse : BaseResponse<IEnumerable<ICD10Chapter>>
    {
        public ICD10ChaptersResponse(IEnumerable<ICD10Chapter> entity) : base(entity)
        {
        }

        public ICD10ChaptersResponse(string errorMessage) : base(errorMessage)
        {
        }
    }
}