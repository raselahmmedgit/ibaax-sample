using lab.scheduler.web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace lab.scheduler.web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            BootStrapper.Run();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            // Get the exception object.
            Exception exception = Server.GetLastError();
            // Handle HTTP errors
            if (exception != null && exception.GetType() == typeof(HttpException))
            {

                ExceptionHelper.Manage(exception, true);

                // The Complete Error Handling Example generates
                // some errors using URLs with "NoCatch" in them;
                // ignore these here to simulate what would happen
                // if a global.asax handler were not implemented.
                if (exception.Message.Contains("NoCatch") || exception.Message.Contains("maxUrlLength"))
                { return; }

            }
            else
            {
                // Log the exception and notify system operators
                if (exception != null)
                {
                    ExceptionHelper.Manage(exception, true);
                }
                
            }

            // Clear the error from the server
            Server.ClearError();
        }

        protected void Application_BeginRequest()
        {

        }

        protected void Application_End()
        {

        }
    }
}
