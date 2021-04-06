using Microsoft.AspNetCore.Mvc;

namespace ProfOsmotr.Web.Controllers
{
    [Route("[controller]")] // АРМ оператора будет доступно по пути /Operator
    public class OperatorController : Controller
    {
        // Любой путь после /Operator/ будет вести к этому действию
        // Маршрутизация будет осуществлаться средствами SPA, внедренного в представление
        [Route("{*anything}")] 
        public IActionResult Index()
        {
            return View();
        }
    }
}