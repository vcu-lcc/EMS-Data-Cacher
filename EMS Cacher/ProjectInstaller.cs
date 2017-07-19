using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;
using System.Linq;
using System.Windows.Forms;

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

        private ServiceController getService(string serviceName)
        {
            if (ServiceController.GetServices().Select(i => i.ServiceName).Contains(serviceName))
            {
                return null;
            }
            ServiceController service = new ServiceController(serviceName);
            if (service.Status == ServiceControllerStatus.ContinuePending || service.Status == ServiceControllerStatus.StartPending)
            {
                service.WaitForStatus(ServiceControllerStatus.Running);
            }
            if (service.Status == ServiceControllerStatus.StopPending)
            {
                service.WaitForStatus(ServiceControllerStatus.Stopped);
            }
            if (service.Status == ServiceControllerStatus.PausePending)
            {
                service.WaitForStatus(ServiceControllerStatus.Paused);
            }
            return service;
        }

        private void ServiceInstaller_AfterInstall(object sender, InstallEventArgs e)
        {
            ServiceController service = getService(this.EmsCacherService.ServiceName);
            if (service == null)
            {
                MessageBox.Show(
                    "The EMS Cacher service was not installed correctly."
                        + Environment.NewLine + "Please reinstall this program.",
                    "An Error Occured",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            else
            {
                if (service.Status != ServiceControllerStatus.Running)
                {
                    service.Start();
                }
            }
        }

        private void serviceInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {
        }

        private void serviceProcessInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {
        }

        protected override void OnBeforeUninstall(IDictionary savedState)
        {
            ServiceController service = getService(this.EmsCacherService.ServiceName);
            if (service != null && service.Status != ServiceControllerStatus.Stopped)
            {
                service.Stop();
            }
        }
    }
}
