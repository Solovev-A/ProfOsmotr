using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProfOsmotr.DAL;
using ProfOsmotr.Web.Infrastructure.Extensions;

namespace ProfOsmotr.Web.Infrastructure
{
    public class AuthorizeAdministratorAndClinicModeratorAttribute : AuthorizeAttribute
    {
        public AuthorizeAdministratorAndClinicModeratorAttribute()
        {
            Roles = string.Join(',', RoleId.Administrator.ToClaimValue(), RoleId.ClinicModerator.ToClaimValue());
        }
    }
}
