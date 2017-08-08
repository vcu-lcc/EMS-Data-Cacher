using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Data;

namespace Templates
{
    public partial class AliasEditorForm : Form
    {
        private enum EDIT_MODE
        {
            CONDITION,
            ACTION,
            ALL
        }
        private ListBox m_conditions = null;
        private ListBox m_actions = null;
        private Button m_addBtn = null;
        private Button m_removeBtn = null;
        private ListBox m_allRules = null;
        private EDIT_MODE m_addMode = EDIT_MODE.ALL;
        Serializable.Array m_aliasConfig = new Serializable.Array();
        Serializable.Object m_currItem = null;

        private Serializable.Object newItem()
        {
            return new Serializable.Object()
                .set("Name", string.Empty)
                .set("Description", string.Empty)
                .set("Conditions", new Serializable.Array())
                .set("Actions", new Serializable.Array());
        }

        public AliasEditorForm()
        {
            InitializeComponent();
            m_currItem = newItem();
        }

        public void refresh()
        {
            while (m_conditions.Items.Count > 0)
            {
                m_conditions.Items.RemoveAt(0);
            }
            while (m_actions.Items.Count > 0)
            {
                m_actions.Items.RemoveAt(0);
            }
            while (m_allRules.Items.Count > 0)
            {
                m_allRules.Items.RemoveAt(0);
            }
            var currentMatches = m_currItem.getArray("Conditions").getChildren();
            foreach (var i in currentMatches)
            {
                m_conditions.Items.Add(i.Item2.ToString());
            }
            var currentActions = m_currItem.getArray("Actions").getChildren();
            foreach (var i in currentActions)
            {
                m_actions.Items.Add(i.Item2.ToString());
            }
            foreach (var i in m_aliasConfig.getChildren())
            {
                Serializable.Object config = (Serializable.Object)i.Item2;
                m_allRules.Items.Add(config.getString("Name") + " - " + config.getString("Description"));
            }
        }

        private void AliasEditorForm_Load(object sender, EventArgs e)
        {
            m_conditions = (ListBox)Controls.Find("LISTBOX_CONDITIONS", true)[0];
            m_actions = (ListBox)Controls.Find("LISTBOX_ACTIONS", true)[0];
            m_addBtn = (Button)Controls.Find("BTN_ADD", true)[0];
            m_removeBtn = (Button)Controls.Find("BTN_REMOVE", true)[0];
            m_allRules = (ListBox)Controls.Find("CURRENT_RULES", true)[0];
            m_conditions.GotFocus += delegate (object unused1, EventArgs unused2)
            {
                m_addBtn.Text = "Add Condition";
                m_addMode = EDIT_MODE.CONDITION;
                m_addBtn.Enabled = true;
                m_actions.ClearSelected();
                m_allRules.ClearSelected();
                m_removeBtn.Text = "Remove";
                m_removeBtn.Enabled = false;
            };
            m_conditions.SelectedIndexChanged += delegate (object unused1, EventArgs unused2)
            {
                if (m_addMode == EDIT_MODE.CONDITION)
                {
                    if (m_conditions.SelectedIndex >= 0)
                    {
                        m_removeBtn.Text = "Remove Condition";
                        m_removeBtn.Enabled = true;
                    }
                    else
                    {
                        m_removeBtn.Text = "Remove";
                        m_removeBtn.Enabled = false;
                    }
                }
            };
            m_actions.GotFocus += delegate (object unused1, EventArgs unused2)
            {
                m_addBtn.Text = "Add Action";
                m_addMode = EDIT_MODE.ACTION;
                m_addBtn.Enabled = true;
                m_conditions.ClearSelected();
                m_allRules.ClearSelected();
                m_removeBtn.Text = "Remove";
                m_removeBtn.Enabled = false;
            };
            m_actions.SelectedIndexChanged += delegate (object unused1, EventArgs unused2)
            {
                if (m_addMode == EDIT_MODE.ACTION)
                {
                    if (m_actions.SelectedIndex >= 0)
                    {
                        m_removeBtn.Text = "Remove Action";
                        m_removeBtn.Enabled = true;
                    }
                    else
                    {
                        m_removeBtn.Text = "Remove";
                        m_removeBtn.Enabled = false;
                    }
                }
            };
            m_allRules.GotFocus += delegate (object unused1, EventArgs unused2)
            {
                m_addBtn.Text = "Add";
                m_addMode = EDIT_MODE.ALL;
                m_addBtn.Enabled = m_actions.Items.Count > 0;
                m_conditions.ClearSelected();
                m_actions.ClearSelected();
                m_removeBtn.Text = "Remove";
                m_removeBtn.Enabled = false;
            };
            m_allRules.SelectedIndexChanged += delegate (object unused1, EventArgs unused2)
            {
                if (m_addMode == EDIT_MODE.ALL)
                {
                    m_removeBtn.Text = "Remove";
                    m_removeBtn.Enabled = m_allRules.SelectedIndex >= 0;
                }
            };
            refresh();
        }

        private void BTN_RESET_Click(object sender, EventArgs e)
        {
            m_currItem = newItem();
            refresh();
            m_addBtn.Text = "Add";
            m_addBtn.Enabled = false;
            m_removeBtn.Text = "Remove";
            m_removeBtn.Enabled = false;
        }

        private void BTN_ADD_Click(object sender, EventArgs e)
        {
            switch (m_addMode)
            {
                case EDIT_MODE.CONDITION:
                    {
                        Serializable.Object newCondition = ConditionEditor.getValue();
                        if (newCondition != null)
                        {
                            m_currItem.getArray("Conditions").add(newCondition);
                        }
                    }
                    break;
                case EDIT_MODE.ACTION:
                    {
                        Serializable.Object newAction = ActionEditor.getValue();
                        if (newAction != null)
                        {
                            m_currItem.getArray("Actions").add(newAction);
                        }
                    }
                    break;
                case EDIT_MODE.ALL:
                    {
                        Dictionary<string, string> value = NameGrabber.getNewValue();
                        if (value != null)
                        {
                            foreach (var i in value)
                            {
                                m_currItem.set(i.Key, i.Value);
                            }
                            m_aliasConfig.add(m_currItem);
                            m_currItem = newItem();
                        }
                    }
                    break;
            }
            refresh();
        }

        private void BTN_REMOVE_Click(object sender, EventArgs e)
        {
            switch (m_addMode)
            {
                case EDIT_MODE.CONDITION:
                    {
                        if (m_conditions.SelectedIndex >= 0)
                        {
                            m_currItem.getArray("Conditions").removeAt(m_conditions.SelectedIndex);
                        }
                    }
                    break;
                case EDIT_MODE.ACTION:
                    {
                        if (m_actions.SelectedIndex >= 0)
                        {
                            m_currItem.getArray("Actions").removeAt(m_actions.SelectedIndex);
                        }
                    }
                    break;
                case EDIT_MODE.ALL:
                    {
                        if (m_allRules.SelectedIndex >= 0)
                        {
                            m_aliasConfig.removeAt(m_allRules.SelectedIndex);
                        }
                    }
                    break;
            }
            refresh();
        }

        private void BTN_OK_Click(object sender, EventArgs e)
        {
            BTN_CANCEL_Click(sender, e);
        }

        private void BTN_CANCEL_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static Serializable.Array edit(Serializable.Array preloadedConfig)
        {
            AliasEditorForm form = new AliasEditorForm();
            form.m_aliasConfig = preloadedConfig;
            form.ShowDialog();
            return form.m_aliasConfig;
        }
    }
}
