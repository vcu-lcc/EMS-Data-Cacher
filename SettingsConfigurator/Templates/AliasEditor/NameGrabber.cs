using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Templates
{
    public partial class NameGrabber : Form
    {
        private TextBox m_InputName = null;
        private TextBox m_InputDescription = null;
        private Button m_ButtonOk = null;
        private Button m_ButtonRemove = null;
        private Dictionary<string, string> m_values = null;

        private NameGrabber()
        {
            InitializeComponent();
        }

        private void NameGrabber_Load(object sender, EventArgs e)
        {
            m_InputName = (TextBox)Controls.Find("INPUT_NAME", true)[0];
            m_InputDescription = (TextBox)Controls.Find("INPUT_DESCRIPTION", true)[0];
            m_ButtonOk = (Button)Controls.Find("BTN_OK", true)[0];
            m_ButtonRemove = (Button)Controls.Find("BTN_CANCEL", true)[0];
            m_InputName.TextChanged += delegate (object unused1, EventArgs unused2)
            {
                m_ButtonOk.Enabled = !string.IsNullOrEmpty(m_InputName.Text) && !string.IsNullOrEmpty(m_InputDescription.Text);
            };
            m_InputDescription.TextChanged += delegate (object unused1, EventArgs unused2)
            {
                m_ButtonOk.Enabled = !string.IsNullOrEmpty(m_InputName.Text) && !string.IsNullOrEmpty(m_InputDescription.Text);
            };
        }

        private void BTN_CANCEL_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BTN_OK_Click(object sender, EventArgs e)
        {
            m_values = new Dictionary<string, string>();
            m_values.Add("Name", m_InputName.Text.Trim());
            m_values.Add("Description", m_InputDescription.Text.Trim());
            Close();
        }

        public static Dictionary<string, string> getNewValue()
        {
            NameGrabber grabber = new NameGrabber();
            grabber.ShowDialog();
            return grabber.m_values;
        }
    }
}
