using lab.scheduler.Helpers;
using lab.scheduler.Schedule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace lab.scheduler
{
    public static class BootStrapper
    {
        public static void Run()
        {
            try
            {
                //ExecuteSchedule();
                //ExecuteMySchedule();
                PingWebSite();
            }
            catch (Exception ex)
            {
                ExceptionHelper.Manage(ex, true);
            }

        }

        private static void ExecuteSchedule()
        {
            try
            {
                QuartzScheduleManager.Daily();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void ExecuteMySchedule()
        {
            try
            {
                TimerScheduleManager timerScheduleManager = new TimerScheduleManager();
                timerScheduleManager.Daily();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void PingWebSite()
        {
            string pingUrl = AppHelper.GetAppSettingsString("PingUrl");

            //using (var webClient = new WebClient())
            //{
            //    var result = webClient.DownloadString(new Uri(pingUrl));
            //}

            WebRequest request = WebRequest.Create(pingUrl);

            var response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                //result OK
            }
            else
            {
                //result NOT OK
            }
        }
    }
}
