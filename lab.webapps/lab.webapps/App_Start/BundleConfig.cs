using System.Web;
using System.Web.Optimization;

namespace lab.webapps
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.10.2.min.js",
                        "~/Scripts/jquery-ui-1.11.4.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.validate.unobtrusive.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-2.6.2.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/kendoui").Include(
                      "~/Scripts/kendo.modernizr.custom.js",
                      "~/Scripts/kendo/2015.1.429/kendo.all.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/appjs").Include(
                        "~/Scripts/app.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/bootstrap.css", new CssRewriteUrlTransform())
                .Include("~/Content/site.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/Content/kendoui")
                      //.Include("~/Content/kendo/2015.1.429/kendo.common.min.css", new CssRewriteUrlTransform())
                      //.Include("~/Content/kendo/2015.1.429/kendo.default.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/kendo/2015.1.429/kendo.common-bootstrap.min.css", new CssRewriteUrlTransform())
                .Include("~/Content/kendo/2015.1.429/kendo.bootstrap.min.css", new CssRewriteUrlTransform()));

            //bundles.Add(new StyleBundle("~/Content/fontawesome").Include(
            //          "~/Content/font-awesome.min.css"));

            bundles.Add(new StyleBundle("~/Content/fontawesome").Include("~/Content/font-awesome.min.css", new CssRewriteUrlTransform()));
            
            BundleTable.EnableOptimizations = true;
        }
    }
}
