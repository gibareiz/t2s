using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace t2s
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Containers",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Containers", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Movimentacoes",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Movimentacoes", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Report",
                url: "{controller}/{action}",
                defaults: new { controller = "Report", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
