﻿using System.Web;
using System.Web.Optimization;

namespace MarkTheWorld
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/app")
                .IncludeDirectory("~/Scripts/libs", "*.js")
                .Include("~/Scripts/app.js")
                .IncludeDirectory("~/Scripts/controllers", "*.js")
                .IncludeDirectory("~/Scripts/services", "*.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/style.css"));
        }
    }
}