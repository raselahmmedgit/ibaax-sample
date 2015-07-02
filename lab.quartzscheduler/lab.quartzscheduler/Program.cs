using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using System.Threading;
using Quartz.Impl;

namespace lab.quartzscheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            #region List Campair
            //string[] collection1 = new string[] { "1", "7", "4" };
            //string[] collection2 = new string[] { "6", "1", "7" };

            //var matchResultSet = collection1.Intersect<string>(collection2);

            //foreach (string s in matchResultSet)
            //{
            //    Console.WriteLine("Match: " + s);
            //}

            //var notMatchResultSet = collection1.Except(collection2);

            //foreach (string s in notMatchResultSet)
            //{
            //    Console.WriteLine("Not Match: " + s);
            //}

            //var result = collection1.Where(p => !collection2.Any(p2 => p2.ID == p.ID));
            #endregion

            #region New Schedule

            try
            {

                // Grab the Scheduler instance from the Factory 
                IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

                scheduler.Start();

                IJobDetail job = JobBuilder.Create<HelloJob>()
                    .WithIdentity("job1", "group1")
                    .Build();

                ////Every 10 Second Later
                //ITrigger trigger = TriggerBuilder.Create()
                //    .WithIdentity("trigger1", "group1")
                //    .StartNow()
                //    .WithSimpleSchedule(x => x
                //        .WithIntervalInSeconds(10)
                //        .RepeatForever())
                //    .Build();

                ////Every 1 Minute Later
                //ITrigger trigger = TriggerBuilder.Create()
                //    .WithIdentity("trigger1", "group1")
                //    .StartNow()
                //    .WithSimpleSchedule(x => x
                //        .WithIntervalInMinutes(1)
                //        .RepeatForever())
                //    .Build();

                ////Every 1 Hours Later
                //ITrigger trigger = TriggerBuilder.Create()
                //    .WithIdentity("trigger1", "group1")
                //    .StartNow()
                //    .WithSimpleSchedule(x => x
                //        .WithIntervalInHours(1)
                //        .RepeatForever())
                //    .Build();

                ////Every Day 3:50 PM = 15:50
                //ITrigger trigger = TriggerBuilder.Create()
                //    .WithIdentity("trigger1", "group1").StartNow().WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(15, 50))
                //    .Build();
                //OR
                //ITrigger trigger = TriggerBuilder.Create()
                //    .WithIdentity("trigger1", "group1").StartNow()
                //    .WithCronSchedule("0 52 15 * * ?").Build();

                //Every Wednesday Day 3:50 PM = 15:50
                //ITrigger trigger = TriggerBuilder.Create().WithIdentity("trigger1", "group1").StartNow().WithSchedule(CronScheduleBuilder
                //.WeeklyOnDayAndHourAndMinute(DayOfWeek.Wednesday, 15, 56)).Build();
                //OR
                ITrigger trigger = TriggerBuilder.Create().StartNow().WithIdentity("trigger1", "group1").WithCronSchedule("0 57 15 ? * WED").Build();


                //ITrigger trigger = TriggerBuilder.Create().StartNow().WithIdentity("trigger1", "group1").WithDailyTimeIntervalSchedule(
                //        s => s.WithIntervalInHours(24).StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(1, 0))).Build();

                scheduler.ScheduleJob(job, trigger);

            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
            }


            #endregion

            #region Old Schedule


            try
            {
                //// Grab the Scheduler instance from the Factory 
                //IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

                //// and start it off
                //scheduler.Start();

                //// some sleep to show what's happening
                //Thread.Sleep(TimeSpan.FromSeconds(60));

                //// and last shut down the scheduler when you are ready to close your program
                //scheduler.Shutdown();

                //// Grab the Scheduler instance from the Factory 
                //IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

                //scheduler.Start();

                //IJobDetail job = JobBuilder.Create<HelloJob>()
                //    .WithIdentity("appNotifyJobDaily", "appNotifyGroupDaily")
                //    .Build();

                //ITrigger trigger = TriggerBuilder.Create().WithIdentity("appNotifyTriggerDaily", "appNotifyGroupDaily").WithDailyTimeIntervalSchedule(
                //        s => s.WithIntervalInHours(24)
                //            .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(5, 0))
                //            .InTimeZone(TimeZoneInfo.Utc))
                //    .Build();

                //scheduler.ScheduleJob(job, trigger);
                

            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
            }

            try
            {
                //Common.Logging.LogManager.Adapter = new Common.Logging.Simple.ConsoleOutLoggerFactoryAdapter { Level = Common.Logging.LogLevel.Info };

                //// Grab the Scheduler instance from the Factory 
                //IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

                //// and start it off
                //scheduler.Start();

                //// define the job and tie it to our HelloJob class
                //IJobDetail job = JobBuilder.Create<HelloJob>()
                //    .WithIdentity("job1", "group1")
                //    .Build();

                //// Trigger the job to run now, and then repeat every 10 seconds
                //ITrigger trigger = TriggerBuilder.Create()
                //    .WithIdentity("trigger1", "group1")
                //    .StartNow()
                //    .WithSimpleSchedule(x => x
                //        .WithIntervalInSeconds(10)
                //        .RepeatForever())
                //    .Build();

                //// Tell quartz to schedule the job using our trigger
                //scheduler.ScheduleJob(job, trigger);

                ////// define the job and tie it to our HelloJob class
                ////IJobDetail emailJob = JobBuilder.Create<HelloJob>()
                ////    .WithIdentity("job2", "group2")
                ////    .Build();

                ////// Trigger the job to run now, and then repeat every 10 seconds
                ////ITrigger emailTrigger = TriggerBuilder.Create()
                ////    .WithIdentity("trigger2", "group2")
                ////    .StartNow()
                ////    .WithSimpleSchedule(x => x
                ////        .WithIntervalInSeconds(10)
                ////        .RepeatForever())
                ////    .Build();

                ////// Tell quartz to schedule the job using our trigger
                ////scheduler.ScheduleJob(emailJob, emailTrigger);

                //// some sleep to show what's happening
                //Thread.Sleep(TimeSpan.FromSeconds(60));

                //// and last shut down the scheduler when you are ready to close your program
                //scheduler.Shutdown();
            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
            }

            #endregion

            Console.WriteLine("Start.....");
            Console.ReadLine();

        }
    }
}
