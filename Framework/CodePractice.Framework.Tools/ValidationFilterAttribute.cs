using System.Runtime.InteropServices.JavaScript;
using CodePractice.Framework.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace CodePractice.Framework.Tools
{
    public class ValidationFilterAttribute : Attribute, IActionFilter
    {
        private readonly bool _validateForId;
        private readonly string _idProperty;

        public ValidationFilterAttribute(bool validateForId = false, string idProperty = "id")
        {
            _validateForId = validateForId;
            _idProperty = idProperty;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(FuncResult.CreateValidationError(context.ModelState));
                return;
            }

            if (_validateForId)
            {
                var request = context.HttpContext.GetRouteValue(_idProperty);
                if (request != null)
                {
                    long? id = long.Parse(request?.ToString());

                    if (id != null && (long)id == 0)
                    {
                        context.Result = new BadRequestObjectResult(FuncResult.CreateValidationError(new Error()
                        {
                            Code = "Validation",
                            Message = "شناسه نمی تواند خالی باشد."
                        }));
                        return;
                    }
                    else if (id == null)
                    {
                        context.Result = new BadRequestObjectResult(FuncResult.CreateValidationError(new Error()
                        {
                            Code = "Validation",
                            Message = "شناسه نمی تواند خالی باشد."
                        }));
                        return;
                    }
                }
            }

        }
        public void OnActionExecuted(ActionExecutedContext context) { }

    }
}