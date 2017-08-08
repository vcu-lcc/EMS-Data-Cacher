using System.Drawing;

namespace Templates
{
    partial class AliasEditorForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.BTN_CANCEL = new System.Windows.Forms.Button();
            this.BTN_OK = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.LISTBOX_CONDITIONS = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LISTBOX_ACTIONS = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.BTN_RESET = new System.Windows.Forms.Button();
            this.BTN_ADD = new System.Windows.Forms.Button();
            this.BTN_REMOVE = new System.Windows.Forms.Button();
            this.CURRENT_RULES = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1888, 992);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.BTN_CANCEL, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.BTN_OK, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 910);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1882, 79);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // BTN_CANCEL
            // 
            this.BTN_CANCEL.AutoSize = true;
            this.BTN_CANCEL.Location = new System.Drawing.Point(1577, 10);
            this.BTN_CANCEL.Margin = new System.Windows.Forms.Padding(10);
            this.BTN_CANCEL.Name = "BTN_CANCEL";
            this.BTN_CANCEL.Size = new System.Drawing.Size(295, 59);
            this.BTN_CANCEL.TabIndex = 0;
            this.BTN_CANCEL.Text = "Cancel";
            this.BTN_CANCEL.UseVisualStyleBackColor = true;
            this.BTN_CANCEL.Click += new System.EventHandler(this.BTN_CANCEL_Click);
            // 
            // BTN_OK
            // 
            this.BTN_OK.AutoSize = true;
            this.BTN_OK.Location = new System.Drawing.Point(1262, 10);
            this.BTN_OK.Margin = new System.Windows.Forms.Padding(10);
            this.BTN_OK.Name = "BTN_OK";
            this.BTN_OK.Size = new System.Drawing.Size(295, 59);
            this.BTN_OK.TabIndex = 1;
            this.BTN_OK.Text = "OK";
            this.BTN_OK.UseVisualStyleBackColor = true;
            this.BTN_OK.Click += new System.EventHandler(this.BTN_OK_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.CURRENT_RULES, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1882, 901);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.LISTBOX_CONDITIONS, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.LISTBOX_ACTIONS, 1, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1876, 386);
            this.tableLayoutPanel5.TabIndex = 2;
            // 
            // LISTBOX_CONDITIONS
            // 
            this.LISTBOX_CONDITIONS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LISTBOX_CONDITIONS.FormattingEnabled = true;
            this.LISTBOX_CONDITIONS.ItemHeight = 31;
            this.LISTBOX_CONDITIONS.Location = new System.Drawing.Point(3, 35);
            this.LISTBOX_CONDITIONS.Name = "LISTBOX_CONDITIONS";
            this.LISTBOX_CONDITIONS.Size = new System.Drawing.Size(932, 348);
            this.LISTBOX_CONDITIONS.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(932, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Conditions";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(941, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(932, 32);
            this.label2.TabIndex = 1;
            this.label2.Text = "Actions";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // LISTBOX_ACTIONS
            // 
            this.LISTBOX_ACTIONS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LISTBOX_ACTIONS.FormattingEnabled = true;
            this.LISTBOX_ACTIONS.ItemHeight = 31;
            this.LISTBOX_ACTIONS.Location = new System.Drawing.Point(941, 35);
            this.LISTBOX_ACTIONS.Name = "LISTBOX_ACTIONS";
            this.LISTBOX_ACTIONS.Size = new System.Drawing.Size(932, 348);
            this.LISTBOX_ACTIONS.TabIndex = 2;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AutoSize = true;
            this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel4.ColumnCount = 4;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.BTN_RESET, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.BTN_ADD, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.BTN_REMOVE, 3, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 395);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1876, 79);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // BTN_RESET
            // 
            this.BTN_RESET.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BTN_RESET.AutoSize = true;
            this.BTN_RESET.Location = new System.Drawing.Point(10, 10);
            this.BTN_RESET.Margin = new System.Windows.Forms.Padding(10);
            this.BTN_RESET.Name = "BTN_RESET";
            this.BTN_RESET.Size = new System.Drawing.Size(295, 59);
            this.BTN_RESET.TabIndex = 4;
            this.BTN_RESET.Text = "Reset";
            this.BTN_RESET.UseVisualStyleBackColor = true;
            this.BTN_RESET.Click += new System.EventHandler(this.BTN_RESET_Click);
            // 
            // BTN_ADD
            // 
            this.BTN_ADD.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BTN_ADD.AutoSize = true;
            this.BTN_ADD.Enabled = false;
            this.BTN_ADD.Location = new System.Drawing.Point(1256, 10);
            this.BTN_ADD.Margin = new System.Windows.Forms.Padding(10);
            this.BTN_ADD.Name = "BTN_ADD";
            this.BTN_ADD.Size = new System.Drawing.Size(295, 59);
            this.BTN_ADD.TabIndex = 3;
            this.BTN_ADD.Text = "Add";
            this.BTN_ADD.UseVisualStyleBackColor = true;
            this.BTN_ADD.Click += new System.EventHandler(this.BTN_ADD_Click);
            // 
            // BTN_REMOVE
            // 
            this.BTN_REMOVE.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.BTN_REMOVE.AutoSize = true;
            this.BTN_REMOVE.Enabled = false;
            this.BTN_REMOVE.Location = new System.Drawing.Point(1571, 10);
            this.BTN_REMOVE.Margin = new System.Windows.Forms.Padding(10);
            this.BTN_REMOVE.Name = "BTN_REMOVE";
            this.BTN_REMOVE.Size = new System.Drawing.Size(295, 59);
            this.BTN_REMOVE.TabIndex = 2;
            this.BTN_REMOVE.Text = "Remove";
            this.BTN_REMOVE.UseVisualStyleBackColor = true;
            this.BTN_REMOVE.Click += new System.EventHandler(this.BTN_REMOVE_Click);
            // 
            // CURRENT_RULES
            // 
            this.CURRENT_RULES.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CURRENT_RULES.FormattingEnabled = true;
            this.CURRENT_RULES.ItemHeight = 31;
            this.CURRENT_RULES.Location = new System.Drawing.Point(3, 512);
            this.CURRENT_RULES.Name = "CURRENT_RULES";
            this.CURRENT_RULES.Size = new System.Drawing.Size(1876, 386);
            this.CURRENT_RULES.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 477);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(1876, 32);
            this.label3.TabIndex = 4;
            this.label3.Text = "All Rules";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // AliasEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1888, 992);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "AliasEditorForm";
            this.Text = "Edit Aliases";
            this.Load += new System.EventHandler(this.AliasEditorForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button BTN_CANCEL;
        private System.Windows.Forms.Button BTN_OK;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button BTN_RESET;
        private System.Windows.Forms.Button BTN_ADD;
        private System.Windows.Forms.Button BTN_REMOVE;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox LISTBOX_ACTIONS;
        private System.Windows.Forms.ListBox LISTBOX_CONDITIONS;
        private System.Windows.Forms.ListBox CURRENT_RULES;
        private System.Windows.Forms.Label label3;
    }
}