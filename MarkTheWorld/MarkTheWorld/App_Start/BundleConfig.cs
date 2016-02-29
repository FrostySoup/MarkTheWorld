using System.Web;
using System.Web.Optimization;

namespace MarkTheWorld
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/app")
                .IncludeDirectory("~/Scripts/squareDetails", "*.js")
                .IncludeDirectory("~/Scripts/shared", "*.js")
                .IncludeDirectory("~/Scripts/shared/simpleModal", "*.js")
                .IncludeDirectory("~/Scripts/shared/toast", "*.js")
                .IncludeDirectory("~/Scripts/topMarkers", "*.js")
                .IncludeDirectory("~/Scripts/googleMapsInitializer", "*.js")
                .IncludeDirectory("~/Scripts/mapSettings", "*.js")
                .IncludeDirectory("~/Scripts/map", "*.js")
                .IncludeDirectory("~/Scripts/map/services", "*.js")
                .IncludeDirectory("~/Scripts/newSquare", "*.js")
                .IncludeDirectory("~/Scripts/account", "*.js")
                .Include("~/Scripts/app.js")
                .IncludeDirectory("~/Scripts/controllers", "*.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/style.css"));
        }
    }
}
