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
    public partial class EMSCacherConfiguartor : Form
    {
        public static string remoteProductName = "EMS Cacher";
        public static string programData = Environment
            .GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + '\\' + remoteProductName + '\\';
        private Serializable.Object configDetails = null;
        private bool saved = true;

        public EMSCacherConfiguartor()
        {
            InitializeComponent();
        }

        private void updateMainView(Object sender, TreeViewEventArgs e)
        {
            TreeView sidePane = (TreeView)this.Controls.Find("BrowseAttributePanel", true)[0];
            FlowLayoutPanel pane = (FlowLayoutPanel)this.Controls.Find("ConfigurationPanel", true)[0];
            Serializable.Object props = (Serializable.Object)e.Node.Tag;

            while (pane.Controls.Count > 0)
            {
                pane.Controls[0].Dispose();
            }
            string descriptionText = props.getString("Description");
            if (!string.IsNullOrEmpty(descriptionText))
            {
                Label header = new Label();
                header.Text = "Description";
                header.Font = new Font(header.Font.FontFamily, 12, FontStyle.Bold);
                header.MaximumSize = new Size(pane.Width - pane.Padding.Horizontal * 2, pane.Height - pane.Padding.Vertical * 2);
                header.AutoSize = true;
                var margin = header.Margin;
                margin.Bottom = 10;
                header.Margin = margin;
                pane.Controls.Add(header);
                Label description = new Label();
                description.Text = descriptionText;
                description.MaximumSize = new Size(pane.Width - pane.Padding.Horizontal * 2, pane.Height - pane.Padding.Vertical * 2);
                description.AutoSize = true;
                pane.Controls.Add(description);
            }
            Serializable.DataType children = props.get("Value");
            TableLayoutPanel table = new TableLayoutPanel();
            table.AutoSize = true;
            int currRow = 0;
            foreach (var i in children.getChildren())
            {
                Serializable.DataType value = ((Serializable.Object)i.Item2).get("Value");
                if (value != null)
                {
                    Label label = new Label();
                    label.AutoSize = true;
                    label.TextAlign = ContentAlignment.MiddleLeft;
                    label.Dock = DockStyle.Fill;
                    label.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
                    label.Text = i.Item1 == null ? currRow.ToString() : i.Item1;
                    Serializable.DataType description = ((Serializable.Object)i.Item2).get("Description");
                    if (description != null && !string.IsNullOrEmpty(description.getValue()))
                    {
                        ToolTip hint = new ToolTip();
                        hint.SetToolTip(label, description.getValue());
                    }
                    if (value is Serializable.Boolean)
                    {
                        CheckBox box = new CheckBox();
                        box.Dock = DockStyle.Fill;
                        box.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
                        box.Checked = ((Serializable.Boolean)value).get();
                        box.CheckedChanged += delegate (Object _sender, EventArgs _e)
                        {
                            this.saved = false;
                            ((Serializable.Object)i.Item2).set("Value", box.Checked);
                        };
                        table.Controls.Add(label, 0, currRow);
                        table.Controls.Add(box, 0, currRow++);
                    }
                    else if (value is Serializable.Number)
                    {
                        TextBox box = new TextBox();
                        box.Dock = DockStyle.Fill;
                        box.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
                        box.Text = value.getValue();
                        box.Width = 100;
                        box.TextChanged += delegate (Object _sender, EventArgs _e) {
                            this.saved = false;
                            double num;
                            if (Double.TryParse(box.Text, out num))
                            {
                                box.BackColor = SystemColors.Window;
                                ((Serializable.Object)i.Item2).set("Value", num);
                            }
                            else
                            {
                                box.BackColor = Color.Red;
                            }
                        };
                        table.Controls.Add(label, 0, currRow);
                        table.Controls.Add(box, 1, currRow++);
                    }
                    else if (value is Serializable.String)
                    {
                        TextBox box = new TextBox();
                        box.Dock = DockStyle.Fill;
                        box.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
                        box.Text = value.getValue();
                        box.Width = 200;
                        box.TextChanged += delegate (Object _sender, EventArgs _e)
                        {
                            this.saved = false;
                            ((Serializable.Object)i.Item2).set("Value", box.Text);
                        };
                        table.Controls.Add(label, 0, currRow);
                        table.Controls.Add(box, 1, currRow++);
                    }
                }
            }
            ColumnStyle nameStyle = new ColumnStyle(SizeType.AutoSize);
            table.ColumnStyles.Add(nameStyle);
            ColumnStyle fieldStyle = new ColumnStyle(SizeType.AutoSize);
            table.ColumnStyles.Add(fieldStyle);
            pane.Controls.Add(table);
        }

        private void setView(Serializable.Object details, TreeNode branch)
        {
            string description = details.getString("Description");
            Serializable.DataType value = details.getObject("Value");
            if (value is Serializable.Object)
            {
                foreach (var i in value.getChildren())
                {
                    if (i.Item2 != null && ((Serializable.Object)i.Item2).get("Value") is Serializable.Object)
                    {
                        Serializable.DataType childValue = ((Serializable.Object)i.Item2).get("Value");
                        if (childValue is Serializable.Object || childValue is Serializable.Array)
                        {
                            TreeNode label = new TreeNode(i.Item1);
                            label.Tag = i.Item2;
                            branch.Nodes.Add(label);
                            setView((Serializable.Object)i.Item2, label);
                        }
                    }
                }
            }
            else if (value is Serializable.Array)
            {
                var children = value.getChildren();
                for (int i = 0; i != children.Count; i++)
                {
                    if (children[i].Item2 != null && ((Serializable.Object)children[i].Item2).get("Value") is Serializable.Object)
                    {
                        Serializable.DataType childValue = ((Serializable.Object)children[i].Item2).get("Value");
                        if (children[i].Item2 is Serializable.Object || children[i].Item2 is Serializable.Array)
                        {
                            TreeNode label = new TreeNode(i.ToString());
                            label.Tag = children[i].Item2;
                            branch.Nodes.Add(label);
                            setView((Serializable.Object)children[i].Item2, label);
                        }
                    }
                }
            }
        }

        private void setView(Serializable.Object obj)
        {
            TreeView pane = (TreeView)this.Controls.Find("BrowseAttributePanel", true)[0];
            while(pane.Nodes.Count > 0)
            {
                pane.Nodes.RemoveAt(0);
            }
            TreeNode root = new TreeNode("Settings");
            root.Tag = obj;
            pane.Nodes.Add(root);
            setView(obj, root);
            pane.SelectedNode = root;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.configDetails = (Serializable.Object)Persistence.configDetails.clone();
            TreeView pane = (TreeView)this.Controls.Find("BrowseAttributePanel", true)[0];
            pane.AfterSelect += updateMainView;
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
            this.setView(configDetails);
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
                if (sc.Status != ServiceControllerStatus.Stopped)
                {
                    sc.Stop();
                }
                sc.WaitForStatus(ServiceControllerStatus.Stopped);
                sc.Start();
            }
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            if (!saved)
            {
                DialogResult result = MessageBox.Show("Save EMS Cacher Settings?", "Alert", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Cancel)
                {
                    return;
                }
                if (result == DialogResult.Yes)
                {
                    this.apply_Click(sender, e);
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
                this.saved = true;
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
                this.setView(configDetails);
            }
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
