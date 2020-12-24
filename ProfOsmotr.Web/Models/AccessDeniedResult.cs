namespace ProfOsmotr.Web.Models
{
    public class AccessDeniedResult : AccessResult
    {
        public AccessDeniedResult() : base("Доступ запрещен")
        {
        }

        public AccessDeniedResult(string message) : base(message)
        {
        }
    }
}