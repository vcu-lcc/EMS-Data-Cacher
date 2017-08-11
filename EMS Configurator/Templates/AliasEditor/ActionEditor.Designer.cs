namespace Templates
{
    partial class ActionEditor
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
            this.SELECTOR_DATATYPE = new System.Windows.Forms.ComboBox();
            this.SELECTOR_OPERATOR = new System.Windows.Forms.ComboBox();
            this.SELECTOR_TYPE = new System.Windows.Forms.ComboBox();
            this.SELECTOR_ATTRIBUTE = new System.Windows.Forms.ComboBox();
            this.INPUTBOX_VALUE = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(988, 210);
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 128);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(982, 79);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // BTN_CANCEL
            // 
            this.BTN_CANCEL.AutoSize = true;
            this.BTN_CANCEL.Location = new System.Drawing.Point(677, 10);
            this.BTN_CANCEL.Margin = new System.Windows.Forms.Padding(10);
            this.BTN_CANCEL.Name = "BTN_CANCEL";
            this.BTN_CANCEL.Size = new System.Drawing.Size(295, 59);
            this.BTN_CANCEL.TabIndex = 1;
            this.BTN_CANCEL.Text = "Cancel";
            this.BTN_CANCEL.UseVisualStyleBackColor = true;
            this.BTN_CANCEL.Click += new System.EventHandler(this.BTN_CANCEL_Click);
            // 
            // BTN_OK
            // 
            this.BTN_OK.AutoSize = true;
            this.BTN_OK.Enabled = false;
            this.BTN_OK.Location = new System.Drawing.Point(362, 10);
            this.BTN_OK.Margin = new System.Windows.Forms.Padding(10);
            this.BTN_OK.Name = "BTN_OK";
            this.BTN_OK.Size = new System.Drawing.Size(295, 59);
            this.BTN_OK.TabIndex = 2;
            this.BTN_OK.Text = "OK";
            this.BTN_OK.UseVisualStyleBackColor = true;
            this.BTN_OK.Click += new System.EventHandler(this.BTN_OK_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 5;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.Controls.Add(this.SELECTOR_DATATYPE, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.SELECTOR_OPERATOR, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.SELECTOR_TYPE, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.SELECTOR_ATTRIBUTE, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.INPUTBOX_VALUE, 4, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(982, 119);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // SELECTOR_DATATYPE
            // 
            this.SELECTOR_DATATYPE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SELECTOR_DATATYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SELECTOR_DATATYPE.Enabled = false;
            this.SELECTOR_DATATYPE.FormattingEnabled = true;
            this.SELECTOR_DATATYPE.Location = new System.Drawing.Point(591, 3);
            this.SELECTOR_DATATYPE.Name = "SELECTOR_DATATYPE";
            this.SELECTOR_DATATYPE.Size = new System.Drawing.Size(190, 39);
            this.SELECTOR_DATATYPE.TabIndex = 5;
            // 
            // SELECTOR_OPERATOR
            // 
            this.SELECTOR_OPERATOR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SELECTOR_OPERATOR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SELECTOR_OPERATOR.FormattingEnabled = true;
            this.SELECTOR_OPERATOR.Location = new System.Drawing.Point(3, 3);
            this.SELECTOR_OPERATOR.Name = "SELECTOR_OPERATOR";
            this.SELECTOR_OPERATOR.Size = new System.Drawing.Size(190, 39);
            this.SELECTOR_OPERATOR.TabIndex = 4;
            // 
            // SELECTOR_TYPE
            // 
            this.SELECTOR_TYPE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SELECTOR_TYPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SELECTOR_TYPE.Enabled = false;
            this.SELECTOR_TYPE.FormattingEnabled = true;
            this.SELECTOR_TYPE.Location = new System.Drawing.Point(199, 3);
            this.SELECTOR_TYPE.Name = "SELECTOR_TYPE";
            this.SELECTOR_TYPE.Size = new System.Drawing.Size(190, 39);
            this.SELECTOR_TYPE.TabIndex = 0;
            // 
            // SELECTOR_ATTRIBUTE
            // 
            this.SELECTOR_ATTRIBUTE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SELECTOR_ATTRIBUTE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SELECTOR_ATTRIBUTE.Enabled = false;
            this.SELECTOR_ATTRIBUTE.FormattingEnabled = true;
            this.SELECTOR_ATTRIBUTE.Location = new System.Drawing.Point(395, 3);
            this.SELECTOR_ATTRIBUTE.Name = "SELECTOR_ATTRIBUTE";
            this.SELECTOR_ATTRIBUTE.Size = new System.Drawing.Size(190, 39);
            this.SELECTOR_ATTRIBUTE.TabIndex = 1;
            // 
            // INPUTBOX_VALUE
            // 
            this.INPUTBOX_VALUE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.INPUTBOX_VALUE.Enabled = false;
            this.INPUTBOX_VALUE.Location = new System.Drawing.Point(787, 3);
            this.INPUTBOX_VALUE.Name = "INPUTBOX_VALUE";
            this.INPUTBOX_VALUE.Size = new System.Drawing.Size(192, 38);
            this.INPUTBOX_VALUE.TabIndex = 3;
            // 
            // ActionEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 210);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ActionEditor";
            this.Text = "New Action";
            this.Load += new System.EventHandler(this.ConditionEditor_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button BTN_CANCEL;
        private System.Windows.Forms.Button BTN_OK;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.ComboBox SELECTOR_TYPE;
        private System.Windows.Forms.ComboBox SELECTOR_ATTRIBUTE;
        private System.Windows.Forms.TextBox INPUTBOX_VALUE;
        private System.Windows.Forms.ComboBox SELECTOR_OPERATOR;
        private System.Windows.Forms.ComboBox SELECTOR_DATATYPE;
    }
}