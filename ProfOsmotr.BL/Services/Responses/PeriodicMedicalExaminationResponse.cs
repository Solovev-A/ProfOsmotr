using ProfOsmotr.DAL;

namespace ProfOsmotr.BL
{
    public class PeriodicMedicalExaminationResponse : BaseResponse<PeriodicMedicalExamination>
    {
        public PeriodicMedicalExaminationResponse(PeriodicMedicalExamination entity) : base(entity)
        {
        }

        public PeriodicMedicalExaminationResponse(string errorMessage) : base(errorMessage)
        {
        }
    }
}