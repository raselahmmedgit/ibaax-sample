using System.IO;
using lab.ngsample.Helpers;
using lab.ngsample.Models;
using System;
using System.Data.Entity;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace lab.ngsample
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

            Log4net();

            InitializeAndSeedDb();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

            // Get the exception object.
            Exception exc = Server.GetLastError();
            // Clear the error from the server
            Server.ClearError();

            // Handle HTTP errors
            if (exc != null && exc.GetType() == typeof(HttpException))
            {
                ExceptionHelper.Manage(exc);
                // The Complete Error Handling Example generates
                // some errors using URLs with "NoCatch" in them;
                // ignore these here to simulate what would happen
                // if a global.asax handler were not implemented.
                if (exc.Message.Contains("NoCatch") || exc.Message.Contains("maxUrlLength"))
                    return;

                if (((HttpException)exc).GetHttpCode() == 404)
                {
                    Context.Server.TransferRequest("/404");
                    //routeData.Values.Add("action", "Error404");
                    //Response.Redirect("/404");
                }
                else if (((HttpException)exc).GetHttpCode() == 403)
                {
                    Context.Server.TransferRequest("/403");
                    //routeData.Values.Add("action", "Error404");
                    //Response.Redirect("/404");
                }
                else
                {
                    Context.Server.TransferRequest("/500");
                }

                //Redirect HTTP errors to HttpError page
                //WebHelper.CurrentSession.Content.ErrorMessage = ExceptionHelper.BuildErrorStack(exc);
                //UrlHelper url = new System.Web.Mvc.UrlHelper(HttpContext.Current.Request.RequestContext);
                //Response.Redirect(url.Action(iBaax.Common.Constants.Actions.Error, iBaax.Common.Constants.Controllers.Home, new { Area="" }));
            }
            else
            {
                ExceptionHelper.Manage(exc);
                if (!HttpContext.Current.Request.Url.ToString().Contains("Error"))
                {
                    Context.Server.TransferRequest("/Error");
                    //UrlHelper url = new System.Web.Mvc.UrlHelper(HttpContext.Current.Request.RequestContext);
                    //Response.Redirect(url.Action(iBaax.Common.Constants.Actions.Error, iBaax.Common.Constants.Controllers.Home, new { Area = "" }));
                }
            }

        }

        private void Log4net()
        {
            try
            {
                log4net.Config.XmlConfigurator.Configure(new FileInfo(System.Web.HttpContext.Current.Server.MapPath("~/Web.config")));

            }
            catch (Exception ex)
            {
                ExceptionHelper.Manage(ex, true);
            }
        }

        #region Method
        private static void InitializeAndSeedDb()
        {
            try
            {
                // Initializes and seeds the database.
                Database.SetInitializer(new DbInitializer());

                using (var context = new AppDbContext())
                {
                    if (!context.Database.Exists())
                    {
                        context.Database.Initialize(force: true);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        #endregion
    }
}
