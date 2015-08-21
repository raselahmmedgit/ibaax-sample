using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using lab.scheduler.web.Helpers;
using Quartz;

namespace lab.scheduler.web.Schedule
{
    public class QuartzScheduleJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                LoggerHelper.InfoLog(("Schedule Execute Start: " + DateTime.Now.ToString("F")));

                LoggerHelper.InfoLog(("TestSchedule() - Start: " + DateTime.Now.ToString("F")));

                TestSchedule();

                LoggerHelper.InfoLog(("TestSchedule() - End: " + DateTime.Now.ToString("F")));

                LoggerHelper.InfoLog(("Schedule Execute End: " + DateTime.Now.ToString("F")));

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
                QuartzScheduleHelper appNotifyHelper = new QuartzScheduleHelper();
                appNotifyHelper.TestSchedule();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}