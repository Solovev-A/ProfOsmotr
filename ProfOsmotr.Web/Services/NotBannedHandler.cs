using Microsoft.AspNetCore.Authorization;
using ProfOsmotr.BL.Abstractions;
using ProfOsmotr.DAL;
using ProfOsmotr.Web.Infrastructure.Extensions;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProfOsmotr.Web.Services
{
    public class NotBannedHandler : AuthorizationHandler<NotBannedRequirement>
    {
        private readonly IClinicService clinicService;

        public NotBannedHandler(IClinicService clinicService)
        {
            this.clinicService = clinicService ?? throw new ArgumentNullException(nameof(clinicService));
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, NotBannedRequirement requirement)
        {
            if (!context.User.Identity.IsAuthenticated || context.User.IsInRole(RoleId.Blocked.ToClaimValue()))
            {
                context.Fail();
            }
            else
            {
                await CheckClinic(context);
            }
            context.Succeed(requirement);
        }

        private async Task CheckClinic(AuthorizationHandlerContext context)
        {
            string clinicIdValue = context.User.FindFirstValue(AuthenticationService.ClinicIdClaimType);

            if (int.TryParse(clinicIdValue, out int clinicId))
            {
                var clinicResponse = await clinicService.GetClinic(clinicId);
                if (!clinicResponse.Succeed)
                {
                    context.Fail();
                }
            }
            else
            {
                context.Fail();
            }
        }
    }
}