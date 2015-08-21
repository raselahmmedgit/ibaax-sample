using lab.scheduler.Helpers;
using System;

namespace lab.scheduler.Schedule
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