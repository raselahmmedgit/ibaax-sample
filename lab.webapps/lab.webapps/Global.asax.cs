using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using lab.webapps.Models;

namespace lab.webapps
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            InitializeAndSeedDb();
        }
        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                _logger.Error("Application_BeginRequest", ex);
            }
        }
        protected void Application_EndRequest(Object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                _logger.Error("Application_BeginRequest", ex);
            }
        }
        protected void Application_Error(Object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                _logger.Error("Application_Error", ex);
            }
        }
        protected void Application_End()
        {
            try
            {

            }
            catch (Exception ex)
            {
                _logger.Error("Application_End", ex);
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
                throw new Exception("Datebase Initialize Error");
            }

        }

        private void RedirectToError()
        {
            UrlHelper url = new UrlHelper(HttpContext.Current.Request.RequestContext);
            Response.Redirect(url.Action("Error", "Home", new { Area = "" }));
        }

        #endregion

    }

}
