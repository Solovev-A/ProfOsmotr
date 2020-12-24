using Microsoft.AspNetCore.Mvc;
using ProfOsmotr.Web.Infrastructure;

namespace ProfOsmotr.Web.Controllers
{
    [AuthorizeAdministrator]
    public class OrderController : Controller
    {
        public IActionResult Examinations()
        {
            return View();
        }

        public IActionResult Items()
        {
            return View();
        }
    }
}