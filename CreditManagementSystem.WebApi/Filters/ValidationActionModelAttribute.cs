using CreditManagementSystem.Common.Exceptions;
using CreditManagementSystem.Common.Extensions;
using CreditManagementSystem.Common.Responses;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CreditManagementSystem.WebApi.Filters
{
    public sealed class ValidationActionModelAttribute : ActionFilterAttribute
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
                var response = new Response<object>
                {
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
                var exception = context.Exception;

                var response = exception is ValidationException exception1 ?
                    exception1.ResponseValidationException() :
                    exception is EventException exception2 ?
                    exception2.ResponseEventException() :
                    exception.ResponseException();

                context.Result = controller.StatusCode(response.Code, response);

                context.Exception = null;
            }
        }
    }
}
