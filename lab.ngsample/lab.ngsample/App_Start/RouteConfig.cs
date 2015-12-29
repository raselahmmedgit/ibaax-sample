using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace lab.ngsample
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              name: "Error",
              url: "Error",
              defaults: new { controller = "Error", action = "Index" }
            );

            routes.MapRoute(
              name: "500",
              url: "500",
              defaults: new { controller = "Error", action = "Error500" }
            );

            routes.MapRoute(
              name: "403",
              url: "403",
              defaults: new { controller = "Error", action = "Error403" }
            );

            routes.MapRoute(
              name: "404",
              url: "404",
              defaults: new { controller = "Error", action = "Error404" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
