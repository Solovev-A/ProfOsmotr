namespace ProfOsmotr.Web.Models
{
    /// <summary>
    /// Результат авторизации
    /// </summary>
    public class AccessResult
    {
        public bool AccessGranted { get; set; }

        public string Message { get; set; }

        public AccessResult()
        {
            AccessGranted = true;
            Message = "OK";
        }

        public AccessResult(string errorMessage)
        {
            AccessGranted = false;
            Message = errorMessage;
        }
    }
}
