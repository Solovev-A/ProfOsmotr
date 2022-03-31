namespace ProfOsmotr.BL
{
    public class ServiceActionResult
    {
        public ServiceActionResult() : this(null)
        {
        }

        public ServiceActionResult(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public bool Succeed => ErrorMessage is null;

        public string ErrorMessage { get; set; }
    }
}