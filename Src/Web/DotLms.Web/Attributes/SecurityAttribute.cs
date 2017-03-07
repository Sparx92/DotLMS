using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DotLms.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class SecurityAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new
            RouteValueDictionary(new { controller = "BackofficeAuthorization", action = "Authorize" }));
        }
    }
}