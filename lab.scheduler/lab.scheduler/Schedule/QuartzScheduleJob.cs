using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using lab.scheduler.Helpers;
using Quartz;

namespace lab.scheduler.Schedule
{
    public class QuartzScheduleJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                LoggerHelper.WriteLog(("Schedule Execute Start: " + DateTime.Now.ToString("F")));

                LoggerHelper.WriteLog(("TestSchedule() - Start: " + DateTime.Now.ToString("F")));

                TestSchedule();

                LoggerHelper.WriteLog(("TestSchedule() - End: " + DateTime.Now.ToString("F")));

                LoggerHelper.WriteLog(("Schedule Execute End: " + DateTime.Now.ToString("F")));

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