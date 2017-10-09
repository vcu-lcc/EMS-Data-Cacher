using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using EducationalInstitution;

namespace Templates
{
    class Action : Serializable.Object
    {
        public enum Operator
        {
            DELETE = 0,
            SET = 1
        }
        public static string[] operatorText =
        {
            "Delete",
            "Set"
        };
        public static Operator[] operators =
        {
            Operator.DELETE,
            Operator.SET
        };
        public static List<Serializable.Object> types = new List<Serializable.Object>
        {
            new University(),
            new Campus(),
            new Building(),
            new Room(),
        };
        private string[] getAttributes(int index)
        {
            return types[index].keys().ToArray();
        }
        public Action(Serializable.Object predefinedProps) : base()
        {
            base.m_children = predefinedProps.getChildren();
        }
        public Action() : base()
        {
        }
        public Action setOperator(Operator op)
        {
            base.set("Operator", (int)op);
            return this;
        }
        public Action setType(string type)
        {
            base.set("Type", type);
            return this;
        }
        public Action setAttribute(string attribute)
        {
            base.set("Attribute", attribute);
            return this;
        }
        public Action setValue(Serializable.DataType value)
        {
            base.set("Value", value);
            return this;
        }
        public void act(params Serializable.Object[] entities)
        {
            Serializable.Object target = null;
            string type = base.getString("Type");
            foreach (Serializable.Object i in entities)
            {
                if (i != null && type == i.getType())
                {
                    target = i;
                    break;
                }
            }
            if (target != null && base.getString("Attribute") != null)
            {
                switch ((Operator)base.getNumber("Operator"))
                {
                    case Operator.DELETE:
                        {
                            target.remove(base.getString("Attribute"));
                            break;
                        }
                    case Operator.SET:
                        {
                            target.set(base.getString("Attribute"), base.get("Value"));
                            break;
                        }
                }
            }
        }
        public override string ToString()
        {
            return (base.getType("Operator") == "Number" ? operatorText[(int)base.getNumber("Operator")] : "") + ' ' + base.getString("Type") + ' ' + base.getString("Attribute") + (base.get("Value") == null ?  "" : " to " + base.get("Value").getValue());
        }
        public override string getType()
        {
            return new Serializable.Object().getType();
        }
    }
}
