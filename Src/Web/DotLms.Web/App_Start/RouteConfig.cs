using System.Web.Mvc;
using System.Web.Routing;

namespace DotLms.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.LowercaseUrls = true;

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "404-NotFound",
                url: "NotFound",
                defaults: new { controller = "Error", action = "NotFound" }
            );

            routes.MapRoute(
                name: "500-Error",
                url: "Error",
                defaults: new { controller = "Error", action = "Error" }
            );

            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "CoursePresentation", action = "Index" });

            routes.MapRoute(
                name: "Course",
                url: "{courseName}",
                defaults: new { controller = "CoursePresentation", action = "GetCourse" });

            routes.MapRoute(
                name: "Page",
                url: "{courseName}/{childPageName}",
                defaults: new { controller = "CoursePresentation", action = "GetPage" });

            routes.MapRoute(
                 name: "NotFound",
                 url: "{*url}",
                 defaults: new { controller = "Error", action = "NotFound" }
            );
        }
    }
}
