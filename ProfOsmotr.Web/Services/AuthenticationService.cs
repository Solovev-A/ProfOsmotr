using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using ProfOsmotr.BL.Abstractions;
using ProfOsmotr.DAL;
using ProfOsmotr.Web.Infrastructure.Extensions;
using ProfOsmotr.Web.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProfOsmotr.Web.Services
{
    public class AuthenticationService : IAuthService
    {
        private const string SCHEME = CookieAuthenticationDefaults.AuthenticationScheme;

        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUserService userService;

        public const string ClinicIdClaimType = "ClinicId";
        public const string UserIdClaimType = "UserId";

        public AuthenticationService(IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task<AuthenticationResult> Authenticate(UserLoginResource resource)
        {
            var response = await userService.ValidatePassword(resource.Username, resource.Password);
            if (!response.Succeed)
                return new AuthenticationResult(response.Message);
            await SignIn(response.Result);
            return new AuthenticationResult();
        }

        public async Task Logout()
        {
            await httpContextAccessor.HttpContext.SignOutAsync(SCHEME);
        }

        private async Task SignIn(User user)
        {
            Claim[] claims = new[]
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.RoleId.ToClaimValue()),
                new Claim(ClinicIdClaimType, user.ClinicId.ToString()),
                new Claim(UserIdClaimType, user.Id.ToString())
            };
            var identity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                                              ClaimsIdentity.DefaultRoleClaimType);
            var principal = new ClaimsPrincipal(identity);
            await httpContextAccessor.HttpContext.SignInAsync(SCHEME, principal);
        }
    }
}