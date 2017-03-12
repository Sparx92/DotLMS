using System.Web.Optimization;

namespace DotLms.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            

            // tinyMce wysiwg editor
            bundles.Add(new ScriptBundle("~/bundles/tinymce")
                .Include("~/Scripts/tinymce/tinymce.min.js"));


            // Unify template

            bundles.Add(new StyleBundle("~/bundles/unify/css")
                .Include("~/Areas/Backoffice/Assets/plugins/bootstrap/css/bootstrap.min.css")
                .Include("~/Areas/Backoffice/Assets/css/style.css")
                .Include("~/Areas/Backoffice/Assets/css/headers/header-default.css"));
            

            bundles.Add(new ScriptBundle("~/bundles/unify/js")
                .Include("~/Areas/Backoffice/Assets/plugins/bootstrap/js/bootstrap.min.js")
                .Include("~/Areas/Backoffice/Assets/plugins/back-to-top.js")
                .Include("~/Areas/Backoffice/Assets/plugins/smoothScroll.js")
                );
        }
    }
}
