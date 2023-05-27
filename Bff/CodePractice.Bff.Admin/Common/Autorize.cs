using CodePractice.Framework.Common.AuditableEntities;
using CodePractice.Framework.Security;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CodePractice.Bff.Admin.Common
{
    public class Autorize : ActionFilterAttribute
    {
        public bool Return { set; get; } = true;
        public string? Key { set; get; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string token = filterContext.HttpContext.Request.Headers["token"];
            if (string.IsNullOrEmpty(token))
            {
                var funcResult = new Framework.Dto.FuncResult() { ID = 0, Status = Framework.Dto.Status.Fail, Error = new Framework.Dto.Error() { Code = "Z01", Message = "Token Is Required" } };
                //filterContext.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                filterContext.Result = new JsonResult(funcResult);

                return;
            }


            IToken t = (IToken)filterContext.HttpContext.RequestServices.GetService(typeof(IToken));

            var result = t.getUserModel(token);

            if (result == null)
            {
                if (Return)
                {
                    var funcResult = new Framework.Dto.FuncResult() { ID = 0, Status = Framework.Dto.Status.Fail, Error = new Framework.Dto.Error() { Code = "Z01", Message = "Token Not Valid" } };
                    //filterContext.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    filterContext.Result = new JsonResult(funcResult);

                    return;
                }
                else
                {
                    filterContext.HttpContext.Items.Add("USERID", null);
                }

            }
            else
            {
                if (!string.IsNullOrEmpty(Key))
                {

                    if (result.Roles.Count(x => x == Key) == 0)
                    {
                        var funcResult = new Framework.Dto.FuncResult()
                        {
                            ID = 0,
                            Status = Framework.Dto.Status.Fail,
                            Error = new Framework.Dto.Error()
                            { Code = "Z01", Message = "Unauthorized" }
                        };

                        filterContext.Result = new JsonResult(funcResult);

                        return;
                    }

                }

                var entity = (filterContext.ActionArguments.FirstOrDefault().Value as AuditableEntity);

                if (filterContext.HttpContext.Request.Method == "POST")
                {

                    if (entity != null)
                    {
                        entity.CreatedById = (int)result.UserID;
                        entity.CreatedOn = DateTime.Now;
                    }

                }

                if (filterContext.HttpContext.Request.Method == "PUT")
                {
                    if (entity != null)
                    {
                        entity.LastModifiedById = (int)result.UserID;
                        entity.LastModifiedOn = DateTime.Now;
                    }
                }

                filterContext.HttpContext.Items.Add("USERID", result.UserID);

            }

        }
    }
}
