using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using Quartz.Impl;
using lab.scheduler.web.Helpers;

namespace lab.scheduler.web.Schedule
{
    public static class QuartzScheduleManager
    {
        #region Global Variable Declaration

        static readonly IScheduler _iScheduler = StdSchedulerFactory.GetDefaultScheduler();

        #endregion

        #region Application Daily Notification

        public static void Daily()
        {
            try
            {
                // Start the scheduler if its in standby
                if (!_iScheduler.IsStarted)
                    _iScheduler.Start();

                bool isScheduleHourWise = AppHelper.GetAppSettingsBoolean("IsScheduleHourWise");
                int dailyTimeInHours = AppHelper.GetAppSettingsInteger("ScheduleDailyTimeInHours");

                int dailyHour = AppHelper.GetAppSettingsInteger("ScheduleDailyHour");
                int dailyMinute = AppHelper.GetAppSettingsInteger("ScheduleDailyMinute");

                IJobDetail job = JobBuilder.Create<QuartzScheduleJob>()
                    .WithIdentity("appNotifyJobDaily", "appNotifyGroupDaily")
                    .Build();

                if (isScheduleHourWise)
                {
                    //Every 1 Hours Later
                    ITrigger trigger = TriggerBuilder.Create()
                        .WithIdentity("appNotifyTriggerDailyInHours", "appNotifyGroupDailyInHours")
                        .StartNow()
                        .WithSimpleSchedule(x => x
                            .WithIntervalInMinutes(dailyTimeInHours)
                            .RepeatForever())
                        .Build();

                    //Validate that the job doesn't already exists
                    CheckExitsJob();

                    _iScheduler.ScheduleJob(job, trigger);
                }
                else
                {
                    //Every Day Time
                    ITrigger trigger = TriggerBuilder.Create()
                        .WithIdentity("appNotifyTriggerDaily", "appNotifyGroupDaily").StartNow().WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(dailyHour, dailyMinute))
                        .Build();

                    //Validate that the job doesn't already exists
                    CheckExitsJob();

                    _iScheduler.ScheduleJob(job, trigger);

                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Check Exit Job

        private static void CheckExitsJob()
        {
            if (_iScheduler.CheckExists(new JobKey("appNotifyJobDaily", "appNotifyGroupDaily")))
            {
                _iScheduler.DeleteJob(new JobKey("appNotifyJobDaily", "appNotifyGroupDaily"));
            }
            else if (_iScheduler.CheckExists(new TriggerKey("appNotifyTriggerDailyInHours", "appNotifyGroupDailyInHours")))
            {

            }
            else if (_iScheduler.CheckExists(new TriggerKey("appNotifyTriggerDaily", "appNotifyGroupDaily")))
            {

            }
        }

        #endregion
    }
}