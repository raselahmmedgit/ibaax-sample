using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using lab.mywebapps.Models;
using lab.mywebapps.Helpers;

namespace lab.mywebapps
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //ModelBinders.Binders.Add(typeof(DateTime), new DateTimeModelBinder());
            //ModelBinders.Binders.Add(typeof(DateTime?), new DateTimeModelBinder());

            ////way of 1
            //var binder = new DateTimeModelBinder(GetCustomDateFormat());
            //ModelBinders.Binders.Add(typeof(DateTime), binder);
            //ModelBinders.Binders.Add(typeof(DateTime?), binder);

            InitializeAndSeedDb();
        }

        ////way of 1
        //private string GetCustomDateFormat()
        //{
        //    return "{0:dd-MMM-yyyy}";
        //}

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            
        }
        protected void Application_EndRequest(Object sender, EventArgs e)
        {
        }
        protected void Application_Error(Object sender, EventArgs e)
        {
        }
        protected void Application_End()
        {
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
