using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Data;

namespace SettingsConfigurator
{
    class AttributesBrowser
    {
        private TreeView m_container = null;
        private DetailsPanel m_detailsPane = null;
        public EventHandler Change;

        protected virtual void OnChange()
        {
            if (Change != null)
            {
                Change(this, new EventArgs());
            }
        }

        public AttributesBrowser(TreeView container, FlowLayoutPanel detailsContainer)
        {
            this.m_container = container;
            this.m_container.AfterSelect += delegate (Object _, TreeViewEventArgs e)
            {
                Serializable.Object props = e.Node.Tag as Serializable.Object;
                this.m_detailsPane.clear();
                if (props != null)
                {
                    this.m_detailsPane.setView(props);
                }
            };
            this.m_detailsPane = new DetailsPanel(detailsContainer);
            this.m_detailsPane.Change += delegate (object unused1, EventArgs unused2)
            {
                this.OnChange();
            };
        }
        private void setView(TreeNode parentNode, Serializable.Object props)
        {
            string description = props.getString("Description");
            Serializable.DataType value = props.get("Value");
            if (value is Serializable.DataType.Object)
            {
                /*
                // To be implemented in next commit
                if (props.get("AddObject") == null || props.getBoolean("AddObject") == true)
                {
                    // Add right click add key option and then refresh
                }
                */
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
