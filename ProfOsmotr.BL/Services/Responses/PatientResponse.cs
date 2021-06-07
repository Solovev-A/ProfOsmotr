using ProfOsmotr.DAL;

namespace ProfOsmotr.BL
{
    public class PatientResponse : BaseResponse<Patient>
    {
        public PatientResponse(Patient entity) : base(entity)
        {
        }

        public PatientResponse(string errorMessage) : base(errorMessage)
        {
        }
    }
}