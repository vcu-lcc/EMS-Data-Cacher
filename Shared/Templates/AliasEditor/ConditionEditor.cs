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
using EducationalInstitution;

namespace Templates
{
    public partial class ConditionEditor : Form
    {
        private ComboBox m_selectorType = null;
        private ComboBox m_selectorAttribute = null;
        private ComboBox m_selectorOperator = null;
        private TextBox m_inputBoxValue = null;
        private Button m_btnOk = null;
        private Condition currCondition = new Condition();
        private string[] m_types =
        {
            new University().getType(),
            new Campus().getType(),
            new Building().getType(),
            new Room().getType()
        };

        public ConditionEditor()
        {
            InitializeComponent();
        }

        private string[] getAttributes(int index)
        {
            return new string[]
            {
                "Name",
                m_types[index] == "Room" ? "Room Number" : "Acronym",
                "ID"
            };
        }

        private void ConditionEditor_Load(object sender, EventArgs e)
        {
            m_selectorType = (ComboBox)Controls.Find("SELECTOR_TYPE", true)[0];
            m_selectorAttribute = (ComboBox)Controls.Find("SELECTOR_ATTRIBUTE", true)[0];
            m_selectorOperator = (ComboBox)Controls.Find("SELECTOR_OPERATOR", true)[0];
            m_inputBoxValue = (TextBox)Controls.Find("INPUTBOX_VALUE", true)[0];
            m_btnOk = (Button)Controls.Find("BTN_OK", true)[0];
            m_selectorType.Items.AddRange(m_types);
            m_selectorType.SelectedIndexChanged += delegate (object unused1, EventArgs unused2)
            {
                while (m_selectorAttribute.Items.Count > 0)
                {
                    m_selectorAttribute.Items.RemoveAt(0);
                }
                m_selectorAttribute.Enabled = false;
                m_selectorAttribute.SelectedIndex = -1;
                m_selectorOperator.Enabled = false;
                m_selectorOperator.SelectedIndex = -1;
                m_inputBoxValue.Enabled = false;
                m_inputBoxValue.Text = "";
                if (m_selectorType.SelectedIndex >= 0)
                {
                    m_selectorAttribute.Enabled = true;
                    m_selectorAttribute.Items.AddRange(getAttributes(m_selectorType.SelectedIndex));
                    currCondition.setType(m_types[m_selectorType.SelectedIndex]);
                }
            };
            m_selectorAttribute.SelectedIndexChanged += delegate (object unused1, EventArgs unused2)
            {
                while (m_selectorOperator.Items.Count > 0)
                {
                    m_selectorOperator.Items.RemoveAt(0);
                }
                m_selectorOperator.Enabled = false;
                m_selectorOperator.SelectedIndex = -1;
                m_inputBoxValue.Enabled = false;
                m_inputBoxValue.Text = "";
                if (m_selectorAttribute.SelectedIndex >= 0)
                {
                    m_selectorOperator.Enabled = true;
                    m_selectorOperator.Items.AddRange(Condition.operatorText);
                    currCondition.setAttribute(getAttributes(m_selectorType.SelectedIndex)[m_selectorAttribute.SelectedIndex]);
                }
            };
            m_selectorOperator.SelectedIndexChanged += delegate (object unused1, EventArgs unused2)
            {
                m_inputBoxValue.Enabled = false;
                m_inputBoxValue.Text = "";
                if (m_selectorOperator.SelectedIndex >= 0)
                {
                    m_inputBoxValue.Enabled = true;
                    currCondition.setOperator(Condition.operators[m_selectorOperator.SelectedIndex]);
                }
            };
            m_inputBoxValue.TextChanged += delegate (object unused1, EventArgs unused2)
            {
                m_btnOk.Enabled = !string.IsNullOrEmpty(m_inputBoxValue.Text);
                currCondition.setValue(m_inputBoxValue.Text);
            };
        }

        public static Serializable.Object getValue()
        {
            ConditionEditor editor = new ConditionEditor();
            editor.ShowDialog();
            return editor.currCondition;
        }

        private void BTN_OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BTN_CANCEL_Click(object sender, EventArgs e)
        {
            currCondition = null;
            this.Close();
        }
    }
}
