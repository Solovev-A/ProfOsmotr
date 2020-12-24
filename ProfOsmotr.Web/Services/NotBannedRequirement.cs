using Microsoft.AspNetCore.Authorization;

namespace ProfOsmotr.Web.Services
{
    public class NotBannedRequirement : IAuthorizationRequirement
    {
    }
}