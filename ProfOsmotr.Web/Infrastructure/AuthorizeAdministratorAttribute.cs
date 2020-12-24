using Microsoft.AspNetCore.Authorization;
using ProfOsmotr.DAL;
using ProfOsmotr.Web.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfOsmotr.Web.Infrastructure
{
    public class AuthorizeAdministratorAttribute : AuthorizeAttribute
    {
        public AuthorizeAdministratorAttribute()
        {
            Roles = RoleId.Administrator.ToClaimValue();
        }
    }
}
