using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using Data;
using XML;
using System.ServiceProcess;

namespace SettingsConfigurator
{
    enum ConfigurationState
    {
        SAVED_APPLIED,  // Configuartion saved and service config up to date
        NOT_APPLIED,    // Configuartion saved, but not the service has not restarted
        NOT_SAVED       // Configuartion not saved and service hasn't restarted; Implies NOT_APPLIED
    }
    public partial class EMSCacherConfigurator : Form
    {
        public static string remoteProductName = "EMS Cacher";
        public static string programData = Environment
            .GetFolderPath(Environment.SpecialFolder.CommonApplicationData)
            + '\\' + remoteProductName + '\\';
        private Serializable.Object configDetails = null;
        private AttributesBrowser panel;
        private ConfigurationState serviceState = ConfigurationState.SAVED_APPLIED;
        
        public EMSCacherConfigurator()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TreeView pane = this.Controls.Find("BrowseAttributePanel", true)[0] as TreeView;
            FlowLayoutPanel mainView = this.Controls.Find("ConfigurationPanel", true)[0] as FlowLayoutPanel;
            this.panel = new AttributesBrowser(pane, mainView);
            this.panel.Change += delegate (object unused1, EventArgs unused2)
            {
                this.serviceState = ConfigurationState.NOT_SAVED;
            };
            this.configDetails = Persistence.configDetails.clone() as Serializable.Object;
            try
            {
                if (File.Exists(programData + "settings.xml"))
                {
                    this.configDetails.apply(
                        Persistence.Config.fatten(
                            (Serializable.Object)Transformations.fromXML(
                                XMLDocument.inflate(
                                    File.ReadAllText(
                                        programData + "settings.xml"
                                    )
                                )
                            )
                        )
                    );
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(
                    err.Message + Environment.NewLine
                        + "Details:" + Environment.NewLine
                        + err.StackTrace,
                    "A fatal error has occured",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            this.panel.setView(configDetails);
        }

        private ServiceController getService(string serviceName)
        {
            if (!ServiceController.GetServices().Select(i => i.ServiceName).Contains(serviceName))
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

        private void promptServiceRestart()
        {
            if (MessageBox.Show(
                "Restart the EMS Cacher service?",
                "Alert",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Asterisk
            ) == DialogResult.Yes)
            {
                ServiceController sc = getService(remoteProductName);
                if (sc == null)
                {
                    MessageBox.Show(
                        "Unable to restart " + remoteProductName + "." + Environment.NewLine
                            + "The specified service does not exist.", "Alert",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
                else
                {
                    if (sc.Status != ServiceControllerStatus.Stopped)
                    {
                        sc.Stop();
                        sc.WaitForStatus(ServiceControllerStatus.Stopped);
                    }
                    sc.Start();
                }
            }
            this.serviceState = ConfigurationState.SAVED_APPLIED;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            if (serviceState == ConfigurationState.NOT_SAVED)
            {
                DialogResult result = MessageBox.Show("Save EMS Cacher Settings?", "Alert", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Cancel)
                {
                    return;
                }
                if (result == DialogResult.Yes)
                {
                    this.ok_Click(sender, e);
                    return;
                }
            }
            this.Close();
        }

        private void apply_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Directory.Exists(programData))
                {
                    Directory.CreateDirectory(programData);
                }
                File.WriteAllText(programData + "settings.xml",
                    Transformations.toXML(
                        Persistence.Config.slimify(
                            this.configDetails
                        )
                    ).ToString()
                );
            }
            catch (IOException err)
            {
                DialogResult result = MessageBox.Show(
                    "Unable to save settings." + Environment.NewLine
                        + "Details:" + Environment.NewLine
                        + err.Message, "Alert",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
            if (this.serviceState != ConfigurationState.SAVED_APPLIED)
            {
                this.serviceState = ConfigurationState.NOT_APPLIED;
            }
        }

        private void ok_Click(object sender, EventArgs e)
        {
            apply_Click(sender, e);
            promptServiceRestart();
            this.Close();
        }

        private void MENU_FILE_IMPORT_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileBrowser = new OpenFileDialog();
            if (fileBrowser.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.configDetails.apply(
                        Persistence.Config.fatten(
                            (Serializable.Object)Transformations.fromXML(
                                XMLDocument.inflate(
                                    File.ReadAllText(
                                        fileBrowser.FileName
                                    )
                                )
                            )
                        )
                    );
                }
                catch (Exception err)
                {
                    MessageBox.Show(
                        err.Message + Environment.NewLine
                            + "Details:" + Environment.NewLine
                            + err.StackTrace,
                        "A fatal error has occured",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
                this.panel.setView(configDetails);
            }
            this.serviceState = ConfigurationState.NOT_SAVED;
        }

        private void MENU_FILE_EXPORT_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "XML Configuration File|*.xml";
                dialog.Title = "Export configuration file...";
                dialog.FileName = "settings";
                dialog.ShowDialog();
                if (dialog.FileName != "")
                {
                    File.WriteAllText(dialog.FileName,
                        Transformations.toXML(
                            Persistence.Config.slimify(
                                this.configDetails
                            )
                        ).ToString()
                    );
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(
                    err.Message + Environment.NewLine
                        + "Details:" + Environment.NewLine
                        + err.StackTrace,
                    "A fatal error has occured",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}
