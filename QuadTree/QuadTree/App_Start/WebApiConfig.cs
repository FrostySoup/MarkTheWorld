using Microsoft.Practices.Unity;
using QuadTree.App_Start.UnityResolver;
using QuadTree.QuadServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace QuadTree
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            var container = new UnityContainer();
            container.RegisterType<IQuadService, QuadService>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolve(container);

            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
