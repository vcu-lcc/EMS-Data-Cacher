using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Data;
using Templates;
using static Persistence;

namespace SettingsConfigurator
{
    class AttributesBrowser
    {
        private TreeView m_container = null;
        private DetailsPanel m_detailsPane = null;
        private FlowLayoutPanel m_detailsPanel = null;
        private ToolStripItem m_btnAdd = null;
        public EventHandler Change;

        protected virtual void OnChange()
        {
            if (Change != null)
            {
                Change(this, new EventArgs());
            }
        }

        private void render(Serializable.Object props)
        {
            this.m_detailsPane = new DetailsPanel(this.m_detailsPanel);
            this.m_detailsPane.Change += delegate (object unused1, EventArgs unused2)
            {
                this.OnChange();
            };
            this.m_detailsPane.clear();
            if (props != null)
            {
                this.m_detailsPane.setView(props);
            }
        }

        public AttributesBrowser(TreeView container, FlowLayoutPanel detailsContainer)
        {
            this.m_container = container;
            this.m_detailsPanel = detailsContainer;
            this.m_btnAdd = this.m_container.FindForm().MainMenuStrip.Items.Find("MENU_EDIT_ADD", true)[0];
            Serializable.Object props = null;
            List<Serializable.DataType> eligible = null;
            TreeNode currNode = null;
            this.m_btnAdd.Click += delegate (object unused1, EventArgs unused2)
            {
                Serializable.DataType[] eligibleDataTypes = eligible.ToArray();
                ValueGrabber grabber = new ValueGrabber(eligibleDataTypes, props.get("Value") is Serializable.Object);
                grabber.ShowDialog();
                Tuple<string, Serializable.DataType> result = grabber.getValue();
                if (result != null)
                {
                    if (props.get("Value") is Serializable.Array)
                    {
                        props.getArray("Value").add(Config.fatten(result.Item2));
                    }
                    else
                    {
                        props.getObject("Value").set(result.Item1, Config.fatten(result.Item2));
                    }
                    if (result.Item2 is Serializable.DataType.Primitive)
                    {
                        this.render(props);
                    }
                    else if (result.Item2 is Serializable.DataType.Object)
                    {
                        this.setView(currNode, props);
                    }
                }
            };
            this.m_container.AfterSelect += delegate (Object _, TreeViewEventArgs e)
            {
                currNode = e.Node;
                props = currNode.Tag as Serializable.Object;
                this.m_btnAdd.Enabled = false;
                eligible = new List<Serializable.DataType>();
                if (props.get("AddPrimitive") == null || props.getBoolean("AddPrimitive") == true)
                {
                    this.m_btnAdd.Enabled = true;
                    eligible.Add(new Serializable.Boolean());
                    eligible.Add(new Serializable.Number());
                    eligible.Add(new Serializable.String());
                }
                if (props.get("AddObject") == null || props.getBoolean("AddObject") == true)
                {
                    this.m_btnAdd.Enabled = true;
                    eligible.Add(new Serializable.Array());
                    eligible.Add(new Serializable.Object());
                }
                this.render(props);
            };
        }
        private void setView(TreeNode parentNode, Serializable.Object props)
        {
            string description = props.getString("Description");
            Serializable.DataType value = props.get("Value");
            if (value is Serializable.DataType.Object)
            {
                while (parentNode.Nodes.Count > 0)
                {
                    parentNode.Nodes.RemoveAt(0);
                }
                int index = 1;
                foreach (var i in value.getChildren())
                {
                    if (i.Item2 is Serializable.DataType.Object)
                    {
                        Serializable.DataType.Object childValue = ((Serializable.Object)i.Item2)
                            .get("Value") as Serializable.DataType.Object;
                        if (childValue != null)
                        {
                            TreeNode label = new TreeNode(
                                value is Serializable.Object ? i.Item1 : "Item " + index.ToString()
                            );
                            label.Tag = i.Item2;
                            parentNode.Nodes.Add(label);
                            setView(label, i.Item2 as Serializable.Object);
                        }
                    }
                    index++;
                }
            }
        }
        public void setView(Serializable.Object props)
        {
            while (m_container.Nodes.Count > 0)
            {
                m_container.Nodes.RemoveAt(0);
            }
            TreeNode root = new TreeNode("Settings");
            root.Tag = props;
            m_container.Nodes.Add(root);
            this.setView(root, props);
            m_container.SelectedNode = root;
        }
    }
}
