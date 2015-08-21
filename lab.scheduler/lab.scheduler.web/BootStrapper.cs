using lab.scheduler.web.Helpers;
using lab.scheduler.web.Schedule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace lab.scheduler.web
{
    public static class BootStrapper
    {
        public static void Run()
        {
            try
            {
                log4net.Config.XmlConfigurator.Configure(new FileInfo(System.Web.HttpContext.Current.Server.MapPath("~/Web.config")));

                ExecuteSchedule();
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
                bool isQuartzSchedule = AppHelper.GetAppSettingsBoolean("IsQuartzSchedule");

                if (isQuartzSchedule)
                {
                    ExecuteQuartzSchedule();
                }
                else
                {
                    ExecuteTimerSchedule();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void ExecuteQuartzSchedule()
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

        private static void ExecuteTimerSchedule()
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
    }
}