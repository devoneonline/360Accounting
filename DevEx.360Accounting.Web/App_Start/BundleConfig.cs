using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace DevEx_360Accounting_Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap-multiselect.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/smartmenus").Include(
                    "~/Scripts/jquery.smartmenus.bootstrap.min.js",
                    "~/Scripts/jquery.smartmenus.min.js"));
            
            
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/syle.css",
                      "~/Content/site.css",
                      "~/Content/jquery-ui.css"));
        }
    }
}