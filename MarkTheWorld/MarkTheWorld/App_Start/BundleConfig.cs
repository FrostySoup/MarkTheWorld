using System.Web;
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
                .IncludeDirectory("~/Scripts/squareDetails", "*.js")
                .IncludeDirectory("~/Scripts/shared", "*.js")
                .IncludeDirectory("~/Scripts/shared/simpleModal", "*.js")
                .IncludeDirectory("~/Scripts/shared/toast", "*.js")
                .IncludeDirectory("~/Scripts/mapSettings", "*.js")
                .IncludeDirectory("~/Scripts/map", "*.js")
                .IncludeDirectory("~/Scripts/map/services", "*.js")
                .IncludeDirectory("~/Scripts/newSquare", "*.js")
                .Include("~/Scripts/app.js")
                .IncludeDirectory("~/Scripts/controllers", "*.js")
                .IncludeDirectory("~/Scripts/services", "*.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/style.css"));
        }
    }
}
