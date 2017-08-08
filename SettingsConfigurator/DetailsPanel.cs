using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Data;
using System.Drawing;
using Templates;

namespace SettingsConfigurator
{
    class DetailsPanel
    {
        private FlowLayoutPanel m_mainView = null;
        public EventHandler Change;

        protected virtual void OnChange()
        {
            if (Change != null)
            {
                Change(this, new EventArgs());
            }
        }

        public DetailsPanel(FlowLayoutPanel mainView)
        {
            this.m_mainView = mainView;
        }
        public void clear()
        {
            while (m_mainView.Controls.Count > 0)
            {
                m_mainView.Controls[0].Dispose();
            }
        }
        private void addKeyValuePair(TableLayoutPanel table, Tuple<string, Serializable.DataType> keyValuePair, int currValue)
        {
            Label label = new Label();
            label.AutoSize = true;
            label.TextAlign = ContentAlignment.MiddleLeft;
            label.Dock = DockStyle.Fill;
            label.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            label.Text = keyValuePair.Item1 == null ? "Item " + currValue : keyValuePair.Item1;
            Serializable.Object valueProps = keyValuePair.Item2 as Serializable.Object;
            Serializable.DataType value = valueProps.get("Value");
            Serializable.DataType description = valueProps.get("Description");
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
                    this.OnChange();
                    valueProps.set("Value", box.Checked);
                };
                table.Controls.Add(label, 0, table.RowCount);
                table.Controls.Add(box, 1, table.RowCount++);
            }
            else if (value is Serializable.Number)
            {
                TextBox box = new TextBox();
                box.Dock = DockStyle.Fill;
                box.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
                box.Text = value.getValue();
                box.Width = 100;
                box.TextChanged += delegate (Object _sender, EventArgs _e) {
                    this.OnChange();
                    double num;
                    if (Double.TryParse(box.Text, out num))
                    {
                        box.BackColor = SystemColors.Window;
                        valueProps.set("Value", num);
                    }
                    else
                    {
                        box.BackColor = Color.Red;
                    }
                };
                table.Controls.Add(label, 0, table.RowCount);
                table.Controls.Add(box, 1, table.RowCount++);
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
                    this.OnChange();
                    valueProps.set("Value", box.Text);
                };
                table.Controls.Add(label, 0, table.RowCount);
                table.Controls.Add(box, 1, table.RowCount++);
            }
            else if (value is Template)
            {
                Button editBtn = new Button();
                editBtn.Text = "Edit";
                editBtn.Click += delegate (object unused1, EventArgs unused2)
                {
                    ((Template)value).invokeEditor();
                };
                table.Controls.Add(label, 0, table.RowCount);
                table.Controls.Add(editBtn, 1, table.RowCount++);
            }
        }

        public void setView(Serializable.Object properties)
        {
            this.clear();
            string descriptionText = properties.getString("Description");
            if (!string.IsNullOrEmpty(descriptionText))
            {
                Label header = new Label();
                header.Text = "Description";
                header.Font = new Font(header.Font.FontFamily, 12, FontStyle.Bold);
                header.MaximumSize = new Size(
                    m_mainView.Width - m_mainView.Padding.Horizontal * 2,
                    m_mainView.Height - m_mainView.Padding.Vertical * 2
                );
                header.AutoSize = true;
                var margin = header.Margin;
                margin.Bottom = 10;
                header.Margin = margin;
                m_mainView.Controls.Add(header);
                Label description = new Label();
                description.Text = descriptionText;
                description.MaximumSize = new Size(
                    m_mainView.Width - m_mainView.Padding.Horizontal * 2,
                    m_mainView.Height - m_mainView.Padding.Vertical * 2
                );
                description.AutoSize = true;
                m_mainView.Controls.Add(description);
            }
            Serializable.DataType children = properties.get("Value");
            TableLayoutPanel table = new TableLayoutPanel();
            table.AutoSize = true;
            ColumnStyle nameStyle = new ColumnStyle(SizeType.AutoSize);
            table.ColumnStyles.Add(nameStyle);
            ColumnStyle fieldStyle = new ColumnStyle(SizeType.AutoSize);
            table.ColumnStyles.Add(fieldStyle);
            int currIndex = 0;
            foreach (var i in children.getChildren())
            {
                if (((Serializable.Object)i.Item2).get("Value") != null)
                {
                    addKeyValuePair(table, i, ++currIndex);
                }
            }
            m_mainView.Controls.Add(table);
        }
    }
}
