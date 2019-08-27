using System.Web.Optimization;

namespace TanCruzDentalInventorySystem
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/moment.min.js",
                        "~/Scripts/erwin.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatables").Include(
                      "~/Scripts/DataTables/DataTables-1.10.18/js/jquery.dataTables.js",
                      "~/Scripts/DataTables/DataTables-1.10.18/js/dataTables.bootstrap4.js",
                      "~/Scripts/DataTables/Buttons-1.5.6/js/dataTables.buttons.js",
                      "~/Scripts/DataTables/Buttons-1.5.6/js/buttons.bootstrap4.js",
                      "~/Scripts/DataTables/Buttons-1.5.6/js/buttons.flash.js",
                      "~/Scripts/DataTables/Buttons-1.5.6/js/buttons.html5.js",
                      "~/Scripts/DataTables/Select-1.3.0/js/dataTables.select.js",
                      "~/Scripts/datatables.extensions/Editor/dataTables.editor.min.js",
                      "~/Scripts/DataTables/Select-1.3.0/js/select.bootstrap.js",
                      "~/Scripts/datetime.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/css/jquery.dataTables.css",
                      "~/Scripts/DataTables/Buttons-1.5.6/css/buttons.bootstrap4.min.css",
                      "~/Scripts/DataTables/Select-1.3.0/css/select.bootstrap.min.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/css/login").Include(
                      "~/Content/Home/login.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            "~/Scripts/jquery.validate*"));
        }
    }
}
