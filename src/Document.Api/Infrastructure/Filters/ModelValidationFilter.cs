using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Document.Api.Infrastructure.Filters
{
    public class ModelValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                return;
            }

            var validationErrors = context.ModelState.Keys
                .SelectMany(k => context.ModelState[k].Errors)
                .Select(e => e.ErrorMessage);

            context.Result = new BadRequestObjectResult(validationErrors);
        }
    }
}
