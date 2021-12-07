namespace ProfOsmotr.Web.Models
{
    public class CreateRegisterRequestQuery : BaseClinicDetailsQuery
    {
        public RegisterRequestSenderQuery User { get; set; }
    }
}