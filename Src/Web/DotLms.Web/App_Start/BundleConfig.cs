using System.Web.Optimization;

namespace DotLms.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;

            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/jquery-{version}.js")
                .Include("~/Scripts/jquery-migrate-{version}.js")
                .Include("~/Scripts/jquery.unobtrusive-ajax.js")
                        );

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

            bundles.Add(new StyleBundle(Common.BundleConstants.UnifyTemplateLoginCssBundle)
                .Include("~/Content/Assets/css/pages/page_log_reg_v2.css")
                );

            bundles.Add(new ScriptBundle(Common.BundleConstants.UnifyTemplateLoginJsBundle)
                .Include("~/Content/Assets/plugins/backstretch/jquery.backstretch.min.js")
                .Include("~/Content/Assets/js/login-page.js")
                );

            bundles.Add(new StyleBundle(Common.BundleConstants.UnifyTemplateCssBundle)
                .Include("~/Content/Assets/plugins/bootstrap/css/bootstrap.min.css")
                .Include("~/Content/Assets/css/style.css")
                .Include("~/Content/Assets/css/headers/header-default.css")
                .Include("~/Content/Assets/plugins/animate.css")
                .Include("~/Content/Assets/css/theme-colors/default.css")
                .Include("~/Content/Assets/css/theme-skins/dark.css")
                .Include("~/Content/Assets/plugins/skyforms/sky-forms.css")
                .Include("~/Content/Assets/plugins/skyforms/custom-sky-forms.css")
                .Include("~/Content/Assets/css/app.css")
                .Include("~/Content/Assets/plugins/cube-portfolio/cubeportfolio/css/cubeportfolio.min.css")
                .Include("~/Content/Assets/plugins/cube-portfolio/cubeportfolio/custom/custom-cubeportfolio.css")
                .Include("~/Content/Assets/css/blocks.css")
                .Include("~/Content/Assets/css/custom.css")
                );

            bundles.Add(new ScriptBundle(Common.BundleConstants.UnifyTemplateJsBundle)
                .Include("~/Content/Assets/plugins/bootstrap/js/bootstrap.js")
                .Include("~/Content/Assets/plugins/back-to-top.js")
                .Include("~/Content/Assets/plugins/smoothScroll.js")
                .Include("~/Content/Assets/plugins/cube-portfolio/cubeportfolio/js/jquery.cubeportfolio.js")
                .Include("~/Content/Assets/plugins/cube-portfolio/cubeportfolio/js/cube-portfolio-3.js")
                .Include("~/Content/Assets/plugins/backstretch/jquery.backstretch.min.js")
                .Include("~/Content/Assets/js/app.js")
                .Include("~/Content/Assets/js/custom.js")
                );
        }
    }
}
