using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace lab.webapps
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            #region My Custom Routes For Web Site

            routes.MapRoute(
                  name: "site",
                  url: "{pageName}",
                  defaults: new { controller = "Page", action = "Index" }
             );

            routes.MapRoute(
                  name: "sitePropertyData",
                  url: "GetProperty/{id}",
                  defaults: new { controller = "Page", action = "GetProperty", id = UrlParameter.Optional }
             );

            routes.MapRoute(
                  name: "siteAgentData",
                  url: "GetAgent/{id}",
                  defaults: new { controller = "Page", action = "GetAgent", id = UrlParameter.Optional }
             );

            #endregion

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Page", action = "Index", id = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //    name: "Account",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            //);
        }
    }
}
