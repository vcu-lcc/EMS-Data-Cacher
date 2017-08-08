using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Data;

namespace Templates
{
    public abstract class Template : Serializable.DataType.Custom
    {
        public override Serializable.DataType clone()
        {
            return this;
        }

        public override bool equal(Serializable.DataType obj)
        {
            return this.serialize().equal(obj.serialize());
        }
        public abstract void invokeEditor();
        public override string getType()
        {
            return new Serializable.Object().getType();
        }
    }
}
