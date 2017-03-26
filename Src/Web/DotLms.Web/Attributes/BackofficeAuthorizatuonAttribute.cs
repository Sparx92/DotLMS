using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace DotLms.Web.Attributes
{
    /// <summary>
    /// Sets authorization level to users who have Admin role
    /// <para>
    /// Role setter has been disabled.
    /// </para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class BackofficeAuthorizatuonAttribute : AuthorizeAttribute
    {
        public BackofficeAuthorizatuonAttribute() : base()
        {
            this.Roles = DotLms.Common.Roles.Admin;
        }

        public string Roles { get; private set; }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new
            RouteValueDictionary(new { controller = "BackofficeAuthorization", action = "Authorize" }));
        }
    }
}