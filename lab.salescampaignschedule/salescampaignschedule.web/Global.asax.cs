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
                _timer = new Timer();
                this._timer.Interval = 30000; //every 30 secs
                this._timer.Elapsed += new System.Timers.ElapsedEventHandler(this._timer_Tick);
                this._timer.Enabled = true;
                _logger.Info("Sales Campaign Schedule Service started.");
                WriteLog("Sales Campaign Schedule Service started.");
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

        private void _timer_Tick(object sender, ElapsedEventArgs e)
        {
            _logger.Info("Timer ticker and some job has been done successfully.");
            WriteLog("Timer ticker and some job has been done successfully.");
        }

        private void WriteLog(string message)
        {
            StreamWriter streamWriter = null;
            //string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //streamWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "Log.txt", true);
            //streamWriter = new StreamWriter(myDocumentsPath + @"\SalesCampaignScheduleServiceLog.txt", true);
            streamWriter = new StreamWriter(@"C:\SalesCampaignScheduleServiceLog.txt", true);
            streamWriter.WriteLine(DateTime.Now.ToString() + " : " + message);
            streamWriter.Flush();
            streamWriter.Close();
        }

        #endregion
    }
}
