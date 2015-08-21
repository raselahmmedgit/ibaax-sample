using lab.scheduler.winservice.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab.scheduler.winservice.Schedule
{
    public class TimerScheduleJob
    {
        public void Execute()
        {
            try
            {
                Console.WriteLine("");
              
                LoggerHelper.WriteLog(("My Schedule Execute Start: " + DateTime.Now.ToString("F")));

                LoggerHelper.WriteLog(("My TestSchedule() - Start: " + DateTime.Now.ToString("F")));

                TestSchedule();

                LoggerHelper.WriteLog(("My TestSchedule() - End: " + DateTime.Now.ToString("F")));

                LoggerHelper.WriteLog(("My Schedule Execute End: " + DateTime.Now.ToString("F")));

            }
            catch (Exception)
            {
                throw;
            }

        }

        #region Test

        private void TestSchedule()
        {
            try
            {
                TimerScheduleHelper appMyScheduleHelper = new TimerScheduleHelper();
                appMyScheduleHelper.TestSchedule();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}