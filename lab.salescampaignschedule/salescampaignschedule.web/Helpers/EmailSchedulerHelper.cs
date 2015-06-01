using System;
using System.IO;
using System.Timers;

namespace salescampaignschedule.web.Helpers
{
    public class EmailSchedulerHelper
    {
        private System.Timers.Timer _timer = null;
        readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public void EmailSchedulerTimer()
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

        private void _timer_Tick(object sender, ElapsedEventArgs e)
        {
            _logger.Info("Timer ticker and some job has been done successfully.");
            WriteLog("Timer ticker and some job has been done successfully.");
        }

        private void WriteLog(string message)
        {
            StreamWriter streamWriter = null;
            //string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            streamWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "Log.txt", true);
            //streamWriter = new StreamWriter(myDocumentsPath + @"\SalesCampaignScheduleServiceLog.txt", true);
            //streamWriter = new StreamWriter(@"C:\SalesCampaignScheduleServiceLog.txt", true);
            streamWriter.WriteLine(DateTime.Now.ToString() + " : " + message);
            streamWriter.Flush();
            streamWriter.Close();
        }
    }
}