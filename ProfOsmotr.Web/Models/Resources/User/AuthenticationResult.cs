namespace ProfOsmotr.Web.Models
{
    public class AuthenticationResult
    {
        public bool Succeed { get; }

        public string Message { get; }

        public AuthenticationResult()
        {
            Succeed = true;
            Message = null;
        }

        public AuthenticationResult(string errorMessage)
        {
            Succeed = false;
            Message = errorMessage;
        }
    }
}