using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace UMS.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.9.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.min.css",
                        "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/signin").Include(
                        "~/Content/signin.css"));

            bundles.Add(new StyleBundle("~/Content/toastr").Include(
                        "~/Content/toastr.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
                        "~/Scripts/toastr.min.js",
                        "~/Scripts/toastrActivator.js"));

            bundles.Add(new ScriptBundle("~/bundles/signalR").Include(
                        "~/Scripts/jquery.signalR-2.2.0.min.js"));

            bundles.Add(new StyleBundle("~/Content/dataTables").Include(
                      "~/Content/dataTables/dataTables.bootstrap.css",
                      "~/Content/dataTables/dataTables.responsive.css",
                      "~/Content/dataTables/dataTables.tableTools.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/dataTables").Include(
                      "~/Scripts/dataTables/jquery.dataTables.js",
                      "~/Scripts/dataTables/dataTables.bootstrap.js",
                      "~/Scripts/dataTables/dataTables.responsive.js",
                      "~/Scripts/dataTables/dataTables.tableTools.min.js"));
        }
    }
}