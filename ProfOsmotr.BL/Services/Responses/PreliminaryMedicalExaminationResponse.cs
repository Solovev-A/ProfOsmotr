using ProfOsmotr.DAL;

namespace ProfOsmotr.BL
{
    public class PreliminaryMedicalExaminationResponse : BaseResponse<PreliminaryMedicalExamination>
    {
        public PreliminaryMedicalExaminationResponse(PreliminaryMedicalExamination entity) : base(entity)
        {
        }

        public PreliminaryMedicalExaminationResponse(string errorMessage) : base(errorMessage)
        {
        }
    }
}