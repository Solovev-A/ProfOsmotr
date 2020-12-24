using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfOsmotr.Web.Infrastructure;
using ProfOsmotr.Web.Services;
using System;
using System.Threading.Tasks;

namespace ProfOsmotr.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IAuthService authService;

        public UserController(IAuthService authService)
        {
            this.authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        [AuthorizeAdministratorAndClinicModerator]
        public IActionResult List()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await authService.Logout();
            return RedirectToAction("Login");
        }
    }
}