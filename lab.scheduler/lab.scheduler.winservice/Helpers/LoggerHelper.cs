using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab.scheduler.winservice.Helpers
{
    public class LoggerHelper
    {
        
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
            string logPath = AppHelper.GetAppSettingsString("LogPath");
            //using (StreamWriter streamWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "logtweeter.txt", true))
            //using (StreamWriter streamWriter = new StreamWriter(myDocumentsPath + @"\logtweeter.txt", true))
            using (StreamWriter streamWriter = new StreamWriter(logPath + @"\logtweeter.txt", true))
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
            string logPath = AppHelper.GetAppSettingsString("LogPath");
            //using (StreamWriter streamWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "logtweeter.txt", true))
            //using (StreamWriter streamWriter = new StreamWriter(myDocumentsPath + @"\logtweeter.txt", true))
            using (StreamWriter streamWriter = new StreamWriter(logPath + @"\logtweeter.txt", true))
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
