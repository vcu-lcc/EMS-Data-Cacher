using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Data;
using Templates;

namespace Templates
{
    class AliasEditor : Template
    {
        private AliasEditorForm m_form = new AliasEditorForm();
        private Serializable.Array m_props = new Serializable.Array();

        public AliasEditor()
        {
        }

        public override Serializable.DataType apply(Serializable.DataType variable)
        {
            Serializable.Array obj = variable.serialize() as Serializable.Array;
            if (obj != null)
            {
                m_props.apply(obj);
            }
            return m_props;
        }
        public override void invokeEditor()
        {
            AliasEditorForm.edit(m_props);
        }
        public override Serializable.DataType ToSerializable()
        {
            return m_props;
        }
    }
}
