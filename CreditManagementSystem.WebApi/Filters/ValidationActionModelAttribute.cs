using CreditManagementSystem.Common.Response;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;

namespace CreditManagementSystem.WebApi.Filters
{
    public class ValidationActionModelAttribute : ActionFilterAttribute
    {
        public ValidationActionModelAttribute()
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Controller is ControllerBase controller && !context.ModelState.IsValid)
                context.Result = controller.BadRequest(context.ModelState);
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is BadRequestObjectResult result && result.Value is ValidationProblemDetails validationProblem)
            {
                var response = new Response<object> {
                    Code = result.StatusCode.Value,
                    Body = validationProblem.Errors,
                    UIText = "Invalid Argument"
                };

                context.Result = new BadRequestObjectResult(response);
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null && context.Controller is ControllerBase controller)
            {
                var response = new Response<object>();

                if (context.Exception is ValidationException exception)
                {
                    response.Code = StatusCodes.Status400BadRequest;
                    response.Body = exception.Errors.Select(e => new Dictionary<string, string> {
                        {e.PropertyName, e.ErrorMessage }
                    });
                    response.UIText = exception.Message;

                    context.Result = controller.StatusCode(StatusCodes.Status400BadRequest, response);
                } else
                {
                    response.Code = StatusCodes.Status500InternalServerError;
                    response.Body = new Dictionary<string, IEnumerable<string>> {
                        { "Fatal error", new string[] { context.Exception.Message } }
                    };
                    response.UIText = "Server Error";

                    context.Result = controller.StatusCode(StatusCodes.Status500InternalServerError, response);
                }

                context.Exception = null;
            }
        }
    }
}
