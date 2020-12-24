using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfOsmotr.Web.Infrastructure;
using ProfOsmotr.Web.Models;

namespace ProfOsmotr.Web.Controllers
{
    public class RegisterController : Controller
    {
        [AllowAnonymous]
        public IActionResult CreateRequest()
        {
            if (User.Identity.IsAuthenticated)
                return Forbid();

            return View();
        }

        [AuthorizeAdministrator]
        public IActionResult Requests()
        {
            return View();
        }
    }
}