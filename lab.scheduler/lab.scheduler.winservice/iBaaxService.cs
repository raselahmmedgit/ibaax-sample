using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace lab.scheduler.winservice
{
    public partial class iBaaxService : ServiceBase
    {
        public iBaaxService()
        {
            InitializeComponent();
            this.ServiceName = "My Windows Service";
            this.EventLog.Log = "iBaax Application";

            // These Flags set whether or not to handle that specific
            //  type of event. Set to true if you need it, false otherwise.
            this.CanHandlePowerEvent = true;
            this.CanHandleSessionChangeEvent = true;
            this.CanPauseAndContinue = true;
            this.CanShutdown = true;
            this.CanStop = true;
        }

        protected override void OnStart(string[] args)
        {
            BootStrapper.Run();
        }

        protected override void OnStop()
        {
        }
    }
}
