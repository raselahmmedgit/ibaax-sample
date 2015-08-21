using lab.scheduler.web.Helpers;
using System;

namespace lab.scheduler.web.Schedule
{
    public class QuartzScheduleHelper
    {
        public void TestSchedule()
        {
            try
            {
                LoggerHelper.InfoLog(("Hello World! I am test scheduler -  :) @raselahmmed : " + DateTime.Now.ToString("F")));
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}