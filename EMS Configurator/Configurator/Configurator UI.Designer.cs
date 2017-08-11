namespace EMS_Configurator
{
    partial class EMSCacherConfigurator
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SettingsManipulator = new System.Windows.Forms.SplitContainer();
            this.BrowseAttributePanel = new System.Windows.Forms.TreeView();
            this.ConfigurationPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.MainUI = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cancel = new System.Windows.Forms.Button();
            this.ok = new System.Windows.Forms.Button();
            this.apply = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MENU_FILE = new System.Windows.Forms.ToolStripMenuItem();
            this.MENU_FILE_IMPORT = new System.Windows.Forms.ToolStripMenuItem();
            this.MENU_FILE_EXPORT = new System.Windows.Forms.ToolStripMenuItem();
            this.MENU_EDIT = new System.Windows.Forms.ToolStripMenuItem();
            this.MENU_EDIT_ADD = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.SettingsManipulator)).BeginInit();
            this.SettingsManipulator.Panel1.SuspendLayout();
            this.SettingsManipulator.Panel2.SuspendLayout();
            this.SettingsManipulator.SuspendLayout();
            this.MainUI.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SettingsManipulator
            // 
            this.SettingsManipulator.BackColor = System.Drawing.SystemColors.Control;
            this.SettingsManipulator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SettingsManipulator.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.SettingsManipulator.Location = new System.Drawing.Point(3, 3);
            this.SettingsManipulator.Name = "SettingsManipulator";
            // 
            // SettingsManipulator.Panel1
            // 
            this.SettingsManipulator.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.SettingsManipulator.Panel1.Controls.Add(this.BrowseAttributePanel);
            this.SettingsManipulator.Panel1.Tag = "";
            this.SettingsManipulator.Panel1MinSize = 150;
            // 
            // SettingsManipulator.Panel2
            // 
            this.SettingsManipulator.Panel2.Controls.Add(this.ConfigurationPanel);
            this.SettingsManipulator.Panel2.Tag = "";
            this.SettingsManipulator.Panel2MinSize = 150;
            this.SettingsManipulator.Size = new System.Drawing.Size(1882, 836);
            this.SettingsManipulator.SplitterDistance = 186;
            this.SettingsManipulator.TabIndex = 0;
            // 
            // BrowseAttributePanel
            // 
            this.BrowseAttributePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BrowseAttributePanel.Location = new System.Drawing.Point(0, 0);
            this.BrowseAttributePanel.Name = "BrowseAttributePanel";
            this.BrowseAttributePanel.Size = new System.Drawing.Size(186, 836);
            this.BrowseAttributePanel.TabIndex = 0;
            // 
            // ConfigurationPanel
            // 
            this.ConfigurationPanel.AutoScroll = true;
            this.ConfigurationPanel.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ConfigurationPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConfigurationPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.ConfigurationPanel.Location = new System.Drawing.Point(0, 0);
            this.ConfigurationPanel.Name = "ConfigurationPanel";
            this.ConfigurationPanel.Padding = new System.Windows.Forms.Padding(10);
            this.ConfigurationPanel.Size = new System.Drawing.Size(1692, 836);
            this.ConfigurationPanel.TabIndex = 0;
            // 
            // MainUI
            // 
            this.MainUI.ColumnCount = 1;
            this.MainUI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.MainUI.Controls.Add(this.SettingsManipulator, 0, 0);
            this.MainUI.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.MainUI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainUI.Location = new System.Drawing.Point(0, 49);
            this.MainUI.Name = "MainUI";
            this.MainUI.RowCount = 2;
            this.MainUI.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.MainUI.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 101F));
            this.MainUI.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.MainUI.Size = new System.Drawing.Size(1888, 943);
            this.MainUI.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.cancel, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.ok, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.apply, 3, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 845);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1882, 95);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // cancel
            // 
            this.cancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cancel.Location = new System.Drawing.Point(1262, 18);
            this.cancel.Margin = new System.Windows.Forms.Padding(10);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(295, 59);
            this.cancel.TabIndex = 11;
            this.cancel.Text = "Cancel";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // ok
            // 
            this.ok.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ok.Location = new System.Drawing.Point(947, 18);
            this.ok.Margin = new System.Windows.Forms.Padding(10);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(295, 59);
            this.ok.TabIndex = 10;
            this.ok.Text = "OK";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // apply
            // 
            this.apply.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.apply.Location = new System.Drawing.Point(1577, 18);
            this.apply.Margin = new System.Windows.Forms.Padding(10);
            this.apply.Name = "apply";
            this.apply.Size = new System.Drawing.Size(295, 59);
            this.apply.TabIndex = 5;
            this.apply.Text = "Apply";
            this.apply.UseVisualStyleBackColor = true;
            this.apply.Click += new System.EventHandler(this.apply_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MENU_FILE,
            this.MENU_EDIT});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1888, 49);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MENU_FILE
            // 
            this.MENU_FILE.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MENU_FILE_IMPORT,
            this.MENU_FILE_EXPORT});
            this.MENU_FILE.Name = "MENU_FILE";
            this.MENU_FILE.Size = new System.Drawing.Size(75, 48);
            this.MENU_FILE.Text = "File";
            // 
            // MENU_FILE_IMPORT
            // 
            this.MENU_FILE_IMPORT.Name = "MENU_FILE_IMPORT";
            this.MENU_FILE_IMPORT.Size = new System.Drawing.Size(222, 46);
            this.MENU_FILE_IMPORT.Text = "Import";
            this.MENU_FILE_IMPORT.Click += new System.EventHandler(this.MENU_FILE_IMPORT_Click);
            // 
            // MENU_FILE_EXPORT
            // 
            this.MENU_FILE_EXPORT.Name = "MENU_FILE_EXPORT";
            this.MENU_FILE_EXPORT.Size = new System.Drawing.Size(222, 46);
            this.MENU_FILE_EXPORT.Text = "Export";
            this.MENU_FILE_EXPORT.Click += new System.EventHandler(this.MENU_FILE_EXPORT_Click);
            // 
            // MENU_EDIT
            // 
            this.MENU_EDIT.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MENU_EDIT_ADD});
            this.MENU_EDIT.Name = "MENU_EDIT";
            this.MENU_EDIT.Size = new System.Drawing.Size(80, 48);
            this.MENU_EDIT.Text = "Edit";
            // 
            // MENU_EDIT_ADD
            // 
            this.MENU_EDIT_ADD.Name = "MENU_EDIT_ADD";
            this.MENU_EDIT_ADD.Size = new System.Drawing.Size(326, 46);
            this.MENU_EDIT_ADD.Text = "Add";
            // 
            // EMSCacherConfigurator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1888, 992);
            this.Controls.Add(this.MainUI);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "EMSCacherConfigurator";
            this.Text = "EMS Cacher Configuartor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SettingsManipulator.Panel1.ResumeLayout(false);
            this.SettingsManipulator.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SettingsManipulator)).EndInit();
            this.SettingsManipulator.ResumeLayout(false);
            this.MainUI.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer SettingsManipulator;
        private System.Windows.Forms.TreeView BrowseAttributePanel;
        private System.Windows.Forms.TableLayoutPanel MainUI;
        private System.Windows.Forms.Button apply;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button ok;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MENU_FILE;
        private System.Windows.Forms.ToolStripMenuItem MENU_FILE_IMPORT;
        private System.Windows.Forms.ToolStripMenuItem MENU_FILE_EXPORT;
        private System.Windows.Forms.FlowLayoutPanel ConfigurationPanel;
        private System.Windows.Forms.ToolStripMenuItem MENU_EDIT;
        private System.Windows.Forms.ToolStripMenuItem MENU_EDIT_ADD;
    }
}

