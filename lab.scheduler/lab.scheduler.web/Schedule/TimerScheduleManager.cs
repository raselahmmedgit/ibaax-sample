using System.Net;
using lab.scheduler.web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Timers;
using System.Web;

namespace lab.scheduler.web.Schedule
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
                this._timer.Interval = 25000; //every 25 secs
                //this._timer.Interval = 30000; //every 30 secs
                //this._timer.Interval = 60000; //every 1 mint
                this._timer.Elapsed += new System.Timers.ElapsedEventHandler(this._timer_Tick);
                this._timer.Enabled = true;
                this._timer.Start();

                _timerTweeter = new Timer();
                this._timerTweeter.Interval = 50000; //every 50 secs
                //this._timerTweeter.Interval = 30000; //every 30 secs
                //this._timerTweeter.Interval = 60000; //every 1 mint
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

            if (_timer != null)
            {
                if (!_timer.Enabled)
                {
                    LoggerHelper.WriteLogTweeter(("Timer is not enabled: " + DateTime.Now.ToString("F")));
                }
                //Always write (_timer.Interval)
                LoggerHelper.WriteLogTweeter(("Timer is not interval: " + _timer.Interval + " Date: " + DateTime.Now.ToString("F")));

                //CallSchedulerAsync().Wait();
                PingWebSite();
            }
        }

        private void _timer_Tick(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();
            _timer.Start();
            TimerScheduleJob timerScheduleJob = new TimerScheduleJob();
            timerScheduleJob.Execute();
        }

        //private static async Task CallSchedulerAsync()
        //{
        //    using (var wc = new WebClient())
        //    {
        //        var result = wc.DownloadString(new Uri("http://localhost:56363/Home/Live"));
        //    }

        //    //using (var client = new HttpClient())
        //    //{
        //    //    client.BaseAddress = new Uri("http://localhost:56363/");
        //    //    client.DefaultRequestHeaders.Accept.Clear();
        //    //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    //    HttpResponseMessage response = await client.GetAsync("api/live/get");
        //    //    if (response.IsSuccessStatusCode)
        //    //    {
        //    //        var result = await response.Content.ReadAsAsync<string>();
        //    //    }
        //    //}
        //}

        private static void PingWebSite()
        {
            string pingUrl = AppHelper.GetAppSettingsString("PingUrl");

            //using (var webClient = new WebClient())
            //{
            //    var result = webClient.DownloadString(new Uri(pingUrl));
            //}

            WebRequest request = WebRequest.Create(pingUrl);

            var response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                //result OK
            }
            else
            {
                //result NOT OK
            }
        }
        #endregion
    }
}