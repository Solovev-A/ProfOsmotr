using ProfOsmotr.DAL;

namespace ProfOsmotr.Web.Infrastructure.Extensions
{
    public static class RoleIdExtensions
    {
        public static string ToClaimValue(this RoleId roleId)
        {
            return roleId.ToString("d");
        }
    }
}