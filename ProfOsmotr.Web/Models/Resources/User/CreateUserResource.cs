using ProfOsmotr.Web.Infrastructure.Validation;

namespace ProfOsmotr.Web.Models
{
    public class CreateUserResource : BaseUserQuery
    {
        [Username]
        public string Username { get; set; }

        public int RoleId { get; set; }
    }
}