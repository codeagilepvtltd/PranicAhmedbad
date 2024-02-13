using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PranicAhmedbad.Common
{
    public class SessionTimeoutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            string Method = context.HttpContext.Request.Method;

            string requestedWith = context.HttpContext.Request.Headers["X-Requested-With"];

            if (requestedWith != null)
            {
                if (requestedWith.Equals("XMLHttpRequest"))
                {
                    if (context.HttpContext.Session == null || !context.HttpContext.Session.TryGetValue("intGlCode", out byte[] val))
                    {
                        context.Result = new JsonResult(new { HttpStatusCode.Unauthorized });
                    }
                    base.OnActionExecuting(context);
                }
            }
            else
            {
                if (context.HttpContext.Session == null || !context.HttpContext.Session.TryGetValue("intGlCode", out byte[] val))
                {
                    context.Result =
                        new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            controller = "Account",
                            action = "Index"
                        }));
                }
                base.OnActionExecuting(context);
            }
        }

    }



}
