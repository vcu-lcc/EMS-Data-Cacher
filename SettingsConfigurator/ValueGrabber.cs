using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Data;

namespace SettingsConfigurator
{
    public partial class ValueGrabber : Form
    {
        private Tuple<string, Serializable.DataType> value = null;
        private Serializable.DataType[] m_types = null;
        private bool m_requireName = true;
        private TextBox name;
        private ComboBox types;
        private Button btnOK;


        public ValueGrabber(Serializable.DataType[] types, bool requireName)
        {
            m_types = types;
            m_requireName = requireName;
            InitializeComponent();
        }

        public ValueGrabber(Serializable.DataType[] types) : this(types, true)
        {
        }

        public Tuple<string, Serializable.DataType> getValue()
        {
            return this.value;
        }

        private void BTN_CANCEL_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BTN_OK_Click(object sender, EventArgs e)
        {
            this.value = new Tuple<string, Serializable.DataType>(
                this.name.Text,
                this.m_types[this.types.SelectedIndex - 1]
            );
            this.Close();
        }

        private void validate(object _sender, EventArgs _e)
        {
            if (this.types.SelectedIndex == 0 || string.IsNullOrWhiteSpace(name.Text) && this.m_requireName)
            {
                btnOK.Enabled = false;
            }
            else
            {
                btnOK.Enabled = true;
            }
        }

        private void ValueGrabber_Load(object sender, EventArgs e)
        {
            this.name = (TextBox)Controls.Find("NameTextBox", true)[0];
            if (!this.m_requireName)
            {
                this.name.Enabled = false;
            }
            this.types = (ComboBox)Controls.Find("TypeComboBox", true)[0];
            this.types.SelectedIndex = 0;
            this.btnOK = (Button)Controls.Find("BTN_OK", true)[0];
            foreach (Serializable.DataType type in m_types)
            {
                this.types.Items.Add(type.getType());
            }
            this.types.SelectedIndexChanged += validate;
            this.name.TextChanged += validate;
        }
    }
}
