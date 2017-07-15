namespace EMS_Cacher
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.EmsServiceInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.EmsCacherService = new System.ServiceProcess.ServiceInstaller();
            // 
            // EmsServiceInstaller
            // 
            this.EmsServiceInstaller.Account = System.ServiceProcess.ServiceAccount.LocalService;
            this.EmsServiceInstaller.Password = null;
            this.EmsServiceInstaller.Username = null;
            this.EmsServiceInstaller.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.serviceProcessInstaller1_AfterInstall);
            // 
            // EmsCacherService
            // 
            this.EmsCacherService.DelayedAutoStart = true;
            this.EmsCacherService.DisplayName = "EMS Cacher";
            this.EmsCacherService.ServiceName = "EMS Cacher";
            this.EmsCacherService.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            this.EmsCacherService.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.serviceInstaller1_AfterInstall);
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.EmsServiceInstaller,
            this.EmsCacherService});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller EmsServiceInstaller;
        private System.ServiceProcess.ServiceInstaller EmsCacherService;
    }
}