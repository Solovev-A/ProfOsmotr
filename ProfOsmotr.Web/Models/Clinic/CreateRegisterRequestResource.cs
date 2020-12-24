namespace ProfOsmotr.Web.Models
{
    public class CreateRegisterRequestResource : BaseClinicDetailsResource
    {
        public RegisterRequestSenderResource User { get; set; }
    }
}