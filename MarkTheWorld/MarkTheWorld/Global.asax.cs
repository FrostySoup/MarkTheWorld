using BusinessLayer.UserService;
using MarkTheWorld.App_Start;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Caching;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MarkTheWorld
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start(Object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RegisterCacheEntry();


        }

        private const string DummyCacheItemKey = "GagaGuguGigi";

        private bool RegisterCacheEntry()
        {
            if (null != HttpContext.Current.Cache[DummyCacheItemKey]) return false;

            HttpContext.Current.Cache.Add(DummyCacheItemKey, "Test", null,
                DateTime.MaxValue, TimeSpan.FromMinutes(10),
                CacheItemPriority.Normal,
                new CacheItemRemovedCallback(CacheItemRemovedCallback));

            return true;
        }

        public void CacheItemRemovedCallback(string key,
             object value, CacheItemRemovedReason reason)
        {
            Debug.WriteLine("Cache item callback: " + DateTime.Now.ToString());

            HitPage();

            // Do the service works

            DoWork();
        }

        private const string DummyPageUrl =
    "http://localhost:59287/dummy";

        private void HitPage()
        {
            WebClient client = new WebClient();
            client.DownloadData(DummyPageUrl);
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {

            if (HttpContext.Current.Request.Url.ToString() == DummyPageUrl)
            {
                // Add the item in cache and when succesful, do the work.

                RegisterCacheEntry();
            }
        }


        private void DoWork()
        {
            if (DateTime.Now.TimeOfDay.Hours == 15 && DateTime.Now.TimeOfDay.Minutes > 15 && DateTime.Now.TimeOfDay.Minutes < 30)
            {
                UserService userService = new UserService();
                userService.renewDailies();
            }
        }


    }

}
