using ProfOsmotr.DAL;

namespace ProfOsmotr.BL
{
    public class ExaminationResultIndexResponse : BaseResponse<ExaminationResultIndex>
    {
        public ExaminationResultIndexResponse(ExaminationResultIndex entity) : base(entity)
        {
        }

        public ExaminationResultIndexResponse(string errorMessage) : base(errorMessage)
        {
        }
    }
}