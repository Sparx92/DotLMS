using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace DotLms.Web.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class BackofficeAuthorizatuonAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new
            RouteValueDictionary(new { controller = "BackofficeAuthorization", action = "Authorize" }));
        }
    }
}