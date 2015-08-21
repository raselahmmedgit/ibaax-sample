using lab.scheduler.winservice.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;

namespace lab.scheduler.winservice.Schedule
{
    public class TimerScheduleManager
    {
        #region Global Variable Declaration

        private Timer _timer = null;
        private Timer _timerTweeter = null;

        #endregion

        #region Application Daily Notification

        public void Daily()
        {
            try
            {
                _timer = new Timer();
                this._timer.Interval = 30000; //every 30 secs
                //this._timer.Interval = 60000; //every 1 mint
                this._timer.Elapsed += new System.Timers.ElapsedEventHandler(this._timer_Tick);
                this._timer.Enabled = true;
                this._timer.Start();

                _timerTweeter = new Timer();
                _timerTweeter.Interval = 60000;
                //_timerTweeter.Interval = 300000;
                this._timerTweeter.Elapsed += new ElapsedEventHandler(this._timerTweeter_Tick);
                this._timerTweeter.Enabled = true;
                this._timerTweeter.AutoReset = true;
                this._timerTweeter.Start();

            }
            catch (Exception)
            {
                throw;
            }
        }

        private void _timerTweeter_Tick(object sender, ElapsedEventArgs e)
        {
            
            if (_timer!=null)
            {
                if (!_timer.Enabled)
                {
                    LoggerHelper.WriteLogTweeter(("Timer is not enabled: " + DateTime.Now.ToString("F")));
                }
                //Always write (_timer.Interval)
                LoggerHelper.WriteLogTweeter(("Timer is not interval: " + _timer.Interval + " Date: " + DateTime.Now.ToString("F")));
                

            }
        }

        private void _timer_Tick(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();
            _timer.Start();
            TimerScheduleJob timerScheduleJob = new TimerScheduleJob();
            timerScheduleJob.Execute();
        }

        #endregion
    }
}