using lab.scheduler.winservice.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab.scheduler.winservice.Schedule
{
    public class TimerScheduleHelper
    {
        public void TestSchedule()
        {
            try
            {
                LoggerHelper.WriteLog(("Hello World! I am test my scheduler -  :) @raselahmmed : " + DateTime.Now.ToString("F")));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}