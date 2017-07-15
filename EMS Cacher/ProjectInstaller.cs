using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;
using System.Linq;

namespace EMS_Cacher
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
            this.AfterInstall += ServiceInstaller_AfterInstall;
        }

        private void ServiceInstaller_AfterInstall(object sender, InstallEventArgs e)
        {
            (new ServiceController(this.EmsCacherService.ServiceName)).Start();
        }

        private void serviceInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {
        }

        private void serviceProcessInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {
        }

        protected override void OnBeforeUninstall(IDictionary savedState)
        {
            (new ServiceController(this.EmsCacherService.ServiceName)).Stop();
        }
    }
}
