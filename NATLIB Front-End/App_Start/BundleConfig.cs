using System.Web;
using System.Web.Optimization;

namespace NATLIB_Front_End
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.12.4.min.js",
                        "~/Scripts/jquery-ui.min.js",
                        "~/Scripts/jquery.easing.1.3.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                      "~/Scripts/mmenu.min.js",
                      "~/Scripts/harvey.min.js",
                      "~/Scripts/waypoints.min.js",
                      "~/Scripts/facts.counter.min.js",
                      "~/Scripts/mixitup.min.js",
                      "~/Scripts/owl.carousel.min.js",
                      "~/Scripts/accordion.min.js",
                      "~/Scripts/responsive.tabs.min.js",
                      "~/Scripts/responsive.table.min.js",
                      "~/Scripts/masonry.min.js",
                      "~/Scripts/carousel.swipe.min.js",
                      "~/Scripts/bxslider.min.js",
                      "~/Scripts/main.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/font-awesome.min.css",
                      "~/Content/mmenu.css",
                      "~/Content/mmenu.positioning.css",
                      "~/Content/style.css"));
        }
    }
}
