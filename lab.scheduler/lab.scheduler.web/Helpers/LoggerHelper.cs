using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace lab.scheduler.web.Helpers
{
    public class LoggerHelper
    {
        static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void ErrorLog()
        {
            Exception ex = HttpContext.Current.Server.GetLastError();
            ErrorLog(ex);
        }

        public static void ErrorLog(Exception ex)
        {
            _logger.Error("Error Log: ", ex);
        }

        public static void InfoLog(Exception ex)
        {
            _logger.Info("Info Log: ", ex);
        }

        public static void InfoLog(string info)
        {
            _logger.Info(info);
        }

        public static void WriteLog(string message)
        {
            //StreamWriter streamWriter = null;
            ////string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //streamWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "log.txt", true);
            ////streamWriter = new StreamWriter(myDocumentsPath + @"\SalesCampaignScheduleServiceLog.txt", true);
            ////streamWriter = new StreamWriter(@"C:\SalesCampaignScheduleServiceLog.txt", true);
            //streamWriter.WriteLine(DateTime.Now.ToString() + " : " + message);
            //streamWriter.Flush();
            //streamWriter.Close();

            //StreamWriter streamWriter = null;
            //string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            using (StreamWriter streamWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "log.txt", true))
            {
                //streamWriter = new StreamWriter(myDocumentsPath + @"\SalesCampaignScheduleServiceLog.txt", true);
                //streamWriter = new StreamWriter(@"C:\SalesCampaignScheduleServiceLog.txt", true);
                streamWriter.WriteLine(DateTime.Now.ToString() + " : " + message);
                streamWriter.Flush();
            }

            //streamWriter.Close();
        }

        public static void WriteLogTweeter(string message)
        {
            //StreamWriter streamWriter = null;
            //string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            using (StreamWriter streamWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "logtweeter.txt", true))
            {
                //streamWriter = new StreamWriter(myDocumentsPath + @"\SalesCampaignScheduleServiceLog.txt", true);
                //streamWriter = new StreamWriter(@"C:\SalesCampaignScheduleServiceLog.txt", true);
                streamWriter.WriteLine(DateTime.Now.ToString() + " : " + message);
                streamWriter.Flush();
            }

            //streamWriter.Close();
        }
    }
}