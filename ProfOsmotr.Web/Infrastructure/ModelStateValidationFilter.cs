using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;

namespace ProfOsmotr.Web.Infrastructure
{
    public class ModelStateValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                List<string> errors = context.ModelState.GetErrorMessages();
                context.Result = new BadRequestObjectResult(errors);
            }

            base.OnActionExecuting(context);
        }
    }
}