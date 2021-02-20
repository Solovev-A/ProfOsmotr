using ProfOsmotr.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfOsmotr.BL
{
    public class ExaminationResultIndexesResponse : BaseResponse<IEnumerable<ExaminationResultIndex>>
    {
        public ExaminationResultIndexesResponse(IEnumerable<ExaminationResultIndex> entity) : base(entity)
        {
        }

        public ExaminationResultIndexesResponse(string errorMessage) : base(errorMessage)
        {
        }
    }
}
