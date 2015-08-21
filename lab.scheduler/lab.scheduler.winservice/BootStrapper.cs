using lab.scheduler.winservice.Helpers;
using lab.scheduler.winservice.Schedule;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab.scheduler.winservice
{
    public static class BootStrapper
    {
        public static void Run()
        {
            try
            {
                //ExecuteSchedule();
                ExecuteMySchedule();
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
                TimerScheduleManager appMySchedule = new TimerScheduleManager();
                appMySchedule.Daily();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
