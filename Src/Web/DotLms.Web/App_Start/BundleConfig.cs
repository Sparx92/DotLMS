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


            // tinyMce wysiwg editor .Include("~/Backoffice/Assets/js/tinyMCE-config.js")
            bundles.Add(new ScriptBundle("~/bundles/tinymce")
                .Include("~/Scripts/tinymce/tinymce.min.js"));


            // Unify template

            bundles.Add(new StyleBundle("~/bundles/unify/backoffice/logincss")
                .Include("~/Areas/Backoffice/Assets/css/pages/page_log_reg_v2.css")
                );

            bundles.Add(new ScriptBundle("~/bundles/unify/backoffice/loginjs")
                .Include("~/Areas/Backoffice/Assets/plugins/backstretch/jquery.backstretch.min.js")
                .Include("~/Areas/Backoffice/Assets/js/login-page.js")
                );

            bundles.Add(new StyleBundle("~/bundles/unify/backoffice/css")
                .Include("~/Areas/Backoffice/Assets/plugins/bootstrap/css/bootstrap.min.css")
                .Include("~/Areas/Backoffice/Assets/css/style.css")
                .Include("~/Areas/Backoffice/Assets/css/headers/header-default.css")
                .Include("~/Areas/Backoffice/Assets/plugins/animate.css")
                .Include("~/Areas/Backoffice/Assets/css/theme-colors/default.css")
                .Include("~/Areas/Backoffice/Assets/css/theme-skins/dark.css")
                .Include("~/Areas/Backoffice/Assets/plugins/skyforms/sky-forms.css")
                .Include("~/Areas/Backoffice/Assets/plugins/skyforms/custom-sky-forms.css")
                .Include("~/Areas/Backoffice/Assets/css/app.css")
                .Include("~/Areas/Backoffice/Assets/css/blocks.css")
                .Include("~/Areas/Backoffice/Assets/css/custom.css")
                );

            bundles.Add(new ScriptBundle("~/bundles/unify/backoffice/js")
                .Include("~/Areas/Backoffice/Assets/plugins/bootstrap/js/bootstrap.min.js")
                .Include("~/Areas/Backoffice/Assets/plugins/back-to-top.js")
                .Include("~/Areas/Backoffice/Assets/plugins/smoothScroll.js")
                .Include("~/Areas/Backoffice/Assets/js/app.js")
                .Include("~/Areas/Backoffice/Assets/js/custom.js")
                );
        }
    }
}
