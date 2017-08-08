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
    public partial class ActionEditor : Form
    {
        private ComboBox m_selectorOperator = null;
        private ComboBox m_selectorType = null;
        private ComboBox m_selectorAttribute = null;
        private ComboBox m_selectorDataType = null;
        private TextBox m_inputBoxValue = null;
        private Button m_btnOk = null;
        private Action currCondition = new Action();
        private string[] m_types =
        {
            new University().getType(),
            new Campus().getType(),
            new Building().getType(),
            new Room().getType()
        };
        private List<Serializable.DataType.Primitive> dataTypes = new List<Serializable.DataType.Primitive>
        {
            new Serializable.Boolean(),
            new Serializable.Number(),
            new Serializable.String()
        };

        public ActionEditor()
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
            m_selectorOperator = (ComboBox)Controls.Find("SELECTOR_OPERATOR", true)[0];
            m_selectorType = (ComboBox)Controls.Find("SELECTOR_TYPE", true)[0];
            m_selectorAttribute = (ComboBox)Controls.Find("SELECTOR_ATTRIBUTE", true)[0];
            m_selectorDataType = (ComboBox)Controls.Find("SELECTOR_DATATYPE", true)[0];
            m_inputBoxValue = (TextBox)Controls.Find("INPUTBOX_VALUE", true)[0];
            m_btnOk = (Button)Controls.Find("BTN_OK", true)[0];
            m_selectorOperator.Items.AddRange(Action.operatorText);
            m_selectorOperator.SelectedIndexChanged += delegate (object unused1, EventArgs unused2)
            {
                while (m_selectorType.Items.Count > 0)
                {
                    m_selectorType.Items.RemoveAt(0);
                }
                while (m_selectorAttribute.Items.Count > 0)
                {
                    m_selectorAttribute.Items.RemoveAt(0);
                }
                while (m_selectorDataType.Items.Count > 0)
                {
                    m_selectorDataType.Items.RemoveAt(0);
                }
                m_selectorType.Enabled = false;
                m_selectorType.SelectedIndex = -1;
                m_selectorAttribute.Enabled = false;
                m_selectorAttribute.SelectedIndex = -1;
                m_selectorDataType.Enabled = false;
                m_selectorDataType.SelectedIndex = -1;
                m_inputBoxValue.Enabled = false;
                m_inputBoxValue.Text = "";
                m_btnOk.Enabled = false;
                if (m_selectorOperator.SelectedIndex >= 0)
                {
                    m_selectorType.Enabled = true;
                    m_selectorType.Items.AddRange(Action.types.Select(i => i.getType()).ToArray());
                    currCondition.setOperator(Action.operators[m_selectorOperator.SelectedIndex]);
                }
            };
            m_selectorType.SelectedIndexChanged += delegate (object unused1, EventArgs unused2)
            {
                while (m_selectorAttribute.Items.Count > 0)
                {
                    m_selectorAttribute.Items.RemoveAt(0);
                }
                while (m_selectorDataType.Items.Count > 0)
                {
                    m_selectorDataType.Items.RemoveAt(0);
                }
                m_selectorAttribute.Enabled = false;
                m_selectorAttribute.SelectedIndex = -1;
                m_selectorDataType.Enabled = false;
                m_selectorDataType.SelectedIndex = -1;
                m_inputBoxValue.Enabled = false;
                m_inputBoxValue.Text = "";
                m_btnOk.Enabled = false;
                if (m_selectorType.SelectedIndex >= 0)
                {
                    m_selectorAttribute.Enabled = true;
                    m_selectorAttribute.Items.AddRange(getAttributes(m_selectorType.SelectedIndex));
                    currCondition.setType(m_types[m_selectorType.SelectedIndex]);
                    if (Action.operators[m_selectorOperator.SelectedIndex] == Action.Operator.DELETE)
                    {
                        m_btnOk.Enabled = true;
                    }
                }
            };
            m_selectorAttribute.SelectedIndexChanged += delegate (object unused1, EventArgs unused2)
            {
                while (m_selectorDataType.Items.Count > 0)
                {
                    m_selectorDataType.Items.RemoveAt(0);
                }
                m_selectorDataType.Enabled = false;
                m_selectorDataType.SelectedIndex = -1;
                m_inputBoxValue.Enabled = false;
                m_inputBoxValue.Text = "";
                m_btnOk.Enabled = false;
                if (m_selectorAttribute.SelectedIndex >= 0)
                {
                    currCondition.setAttribute(getAttributes(m_selectorType.SelectedIndex)[m_selectorAttribute.SelectedIndex]);
                    if (Action.operators[m_selectorOperator.SelectedIndex] == Action.Operator.DELETE)
                    {
                        m_btnOk.Enabled = true;
                    }
                    else
                    {
                        m_selectorDataType.Enabled = true;
                        m_selectorDataType.Items.AddRange(dataTypes.Select(i => i.getType()).ToArray());
                    }
                }
            };
            m_selectorDataType.SelectedIndexChanged += delegate (object unused1, EventArgs unused2)
            {
                m_inputBoxValue.Enabled = false;
                m_inputBoxValue.Text = "";
                m_btnOk.Enabled = false;
                if (m_selectorDataType.SelectedIndex >= 0)
                {
                    m_inputBoxValue.Enabled = true;
                }
            };
            m_inputBoxValue.TextChanged += delegate (object unused1, EventArgs unused2)
            {
                if (m_selectorDataType.SelectedIndex >= 0)
                {
                    m_btnOk.Enabled = !string.IsNullOrEmpty(m_inputBoxValue.Text);
                    currCondition.setValue(dataTypes[m_selectorDataType.SelectedIndex].fromString(m_inputBoxValue.Text.Trim()));
                }
            };
        }

        public static Serializable.Object getValue()
        {
            ActionEditor editor = new ActionEditor();
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
