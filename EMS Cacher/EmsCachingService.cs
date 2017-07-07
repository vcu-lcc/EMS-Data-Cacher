using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace EMS_Cacher
{
    public partial class EmsCachingService : ServiceBase
    {
        public EmsCachingService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            EventLog.WriteEntry("Starting EMSCacher on " + DateTime.Now.ToString("MMMM dd, yyyy hh:mm:ss tt"));
        }

        protected override void OnStop()
        {
        }
    }
}
