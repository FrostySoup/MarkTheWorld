using System.Web;
using System.Web.Optimization;

namespace MarkTheWorld
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/app")
                .IncludeDirectory("~/Scripts/libs", "*.js")
                .IncludeDirectory("~/Scripts/squareDetails", "*.js")
                .IncludeDirectory("~/Scripts/shared", "*.js")
                .IncludeDirectory("~/Scripts/shared/simpleModal", "*.js")
                .IncludeDirectory("~/Scripts/shared/confirmDialog", "*.js")
                .IncludeDirectory("~/Scripts/shared/toast", "*.js")
                .IncludeDirectory("~/Scripts/topMarkers", "*.js")
                .IncludeDirectory("~/Scripts/googleMapsInitializer", "*.js")
                .IncludeDirectory("~/Scripts/topToolbar", "*.js")
                .IncludeDirectory("~/Scripts/sideNav", "*.js")
                .IncludeDirectory("~/Scripts/sideNav/directives", "*.js")
                .IncludeDirectory("~/Scripts/mapSettings", "*.js")
                .IncludeDirectory("~/Scripts/myProfile", "*.js")
                .IncludeDirectory("~/Scripts/myProfile/directives", "*.js")
                .IncludeDirectory("~/Scripts/myProfile/myProfilePicture", "*.js")
                .IncludeDirectory("~/Scripts/myProfile/myProfileColor", "*.js")
                .IncludeDirectory("~/Scripts/myProfile/myProfileColor/directives", "*.js")
                .IncludeDirectory("~/Scripts/map", "*.js")
                .IncludeDirectory("~/Scripts/map/services", "*.js")
                .IncludeDirectory("~/Scripts/newSquare", "*.js")
                .IncludeDirectory("~/Scripts/account", "*.js")
                .Include("~/Scripts/app.js")
                .Include("~/Scripts/appController.js"));

            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/styles/css/app.css")
                .Include("~/Content/styles/css/libs/ng-img-crop.css")
                );
        }
    }
}
