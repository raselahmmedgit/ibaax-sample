using lab.scheduler.winservice.Helpers;
using System;

namespace lab.scheduler.winservice.Schedule
{
    public class QuartzScheduleHelper
    {
        public void TestSchedule()
        {
            try
            {
                LoggerHelper.WriteLog(("Hello World! I am test scheduler -  :) @raselahmmed : " + DateTime.Now.ToString("F")));
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}