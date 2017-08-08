using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace EMS_Cacher
{
    public partial class EmsCachingService : ServiceBase
    {
        private Thread t = null;
        private static string productName = System.Windows.Forms.Application.ProductName;
        private EventLog log;
        private static EmsCachingService currService = null;

        public EmsCachingService()
        {
            InitializeComponent();
            this.log = new EventLog();
            this.log.Source = productName;
        }

        private void startMain()
        {
            string programData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)
                + '\\' + productName + '\\';
            EMS_Cacher.EMSCacher.start(new String[] {
                programData + "settings.xml"
            });
        }

        protected override void OnStart(string[] args)
        {
            this.t = new Thread(new ThreadStart(startMain));
            this.t.Start();
        }

        protected override void OnStop()
        {
            this.t.Abort();
            this.t = null;
        }

        public static void start()
        {
            if (currService == null)
            {
                currService = new EmsCachingService();
            }
            if (currService.t == null)
            {
                currService.OnStart(new String[0]);
            }
        }
        public static void stop()
        {
            if (currService != null && currService.t != null)
            {
                currService.OnStop();
            }
        }
    }
}
