using Domain.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace API.Validators
{
    public class ValidatorActionFilter : Microsoft.AspNetCore.Mvc.Filters.IActionFilter
    {
        private readonly ILogger<ValidatorActionFilter> _logger;

        public ValidatorActionFilter(ILogger<ValidatorActionFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var attributes = descriptor?.MethodInfo.CustomAttributes ?? Enumerable.Empty<System.Reflection.CustomAttributeData>();

            if (!context.ModelState.IsValid && !attributes.Any(a => a.AttributeType == typeof(IgnoreActionFilter)))
            {
                var mensagens = string.Join(" ", context.ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage));

                _logger.LogError(mensagens);

                context.Result = new BadRequestObjectResult(new BaseResponse<string>
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = mensagens
                });
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult result && result.Value != null)
            {
                var value = result.Value;
                string? message = value.GetType().GetProperty("Message")?.GetValue(value, null) as string;
                int? statusCode = value.GetType().GetProperty("StatusCode")?.GetValue(value, null) as int?;

                if (!string.IsNullOrEmpty(message))
                {
                    switch (statusCode)
                    {
                        case 400:
                            _logger.LogError(message);
                            break;
                        case 404:
                            _logger.LogWarning(message);
                            break;
                    }
                }
            }
        }

        [AttributeUsage(AttributeTargets.Method)]
        public class IgnoreActionFilter : Attribute
        {
        }
    }
}
