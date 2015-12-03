using System.IO;
using lab.ngsample.Helpers;
using lab.ngsample.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
