using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using salescampaignschedule.web.Helpers;
using salescampaignschedule.web.Models;

namespace salescampaignschedule.web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private System.Timers.Timer _timer = null;
        readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            InitializeAndSeedDb();
            SchedulerTimer();

        }

        #region Method
        private void SchedulerTimer()
        {
            try
            {
                var emailSchedulerHelper = new EmailSchedulerHelper();
                emailSchedulerHelper.EmailSchedulerTimer();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

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
