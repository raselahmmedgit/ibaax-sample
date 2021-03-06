﻿using RnD.iBaaxData;
using System;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace RnD.iBaaxWebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            InitializeAndSeedDb();
        }

        private static void InitializeAndSeedDb()
        {
            try
            {
                // Initializes and seeds the database.
                Database.SetInitializer(new DBInitializer());

                using (var context = new AppDbContext())
                {
                    context.Database.Initialize(force: true);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
