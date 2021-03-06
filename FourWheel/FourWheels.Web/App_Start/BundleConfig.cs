﻿using System.Web.Optimization;

namespace FourWheels.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/Kendo/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/unobtrusiveJQ").Include(
                        "~/Scripts/Kendo/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                      "~/Scripts/Kendo/jquery.min.js",
                      "~/Scripts/Kendo/kendo.web.min.js",
                      "~/Scripts/Kendo/kendo.aspnetmvc.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/basicAad").Include(
                      "~/Content/basic-ad.css"));

            bundles.Add(new StyleBundle("~/Content/kendoCss").Include(
                      "~/Content/Kendo/kendo.bootstrap.min.css",
                      "~/Content/Kendo/kendo.common.min.css"));


            bundles.Add(new StyleBundle("~/Content/welcomeLayout").Include(
                      "~/Content/welcomeLayout.css"));

        }
    }
}
