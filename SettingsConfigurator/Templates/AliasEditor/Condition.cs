using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using EducationalInstitution;

namespace Templates
{
    class Condition : Serializable.Object
    {
        public enum Operator
        {
            EQUAL = 0,
            NOT_EQUAL = 1,
            CONTAIN = 2,
            NOT_CONTAIN = 3,
            START_WITH = 4,
            ENDS_WITH = 5
        }
        public static string[] operatorText =
        {
            "is",
            "is not",
            "contains",
            "does not contain",
            "start with",
            "ends with"
        };
        public static Operator[] operators =
        {
            Operator.EQUAL,
            Operator.NOT_EQUAL,
            Operator.CONTAIN,
            Operator.NOT_CONTAIN,
            Operator.START_WITH,
            Operator.ENDS_WITH
        };
        public Condition(Serializable.Object predefinedProps) : base()
        {
            base.m_children = predefinedProps.getChildren();
        }
        public Condition() : base()
        {
        }
        public Condition setType(string type)
        {
            base.set("Type", type);
            return this;
        }
        public Condition setAttribute(string attribute)
        {
            base.set("Attribute", attribute);
            return this;
        }
        public Condition setOperator(Operator op)
        {
            base.set("Operator", (int)op);
            return this;
        }
        public Condition setValue(string value)
        {
            base.set("Value", value);
            return this;
        }
        public bool test(University university, Campus campus, Building building, Room room)
        {
            Serializable.Object target = null;
            string type = base.getString("Type");
            if (university != null && university.getType() == type)
            {
                target = university;
            }
            else if (campus != null && campus.getType() == type)
            {
                target = campus;
            }
            else if (building != null && building.getType() == type)
            {
                target = building;
            }
            else if (room != null && room.getType() == type)
            {
                target = room;
            }
            if (target != null && base.getString("Attribute") != null)
            {
                Serializable.DataType remoteAttr = target.get(base.getString("Attribute"));
                string first = remoteAttr == null ? "" : remoteAttr.getValue();
                string second = base.get("Value").getValue();
                switch((Operator)base.getNumber("Operator"))
                {
                    case Operator.EQUAL:
                        {
                            return first == second;
                        }
                    case Operator.NOT_EQUAL:
                        {
                            return first != second;
                        }
                    case Operator.CONTAIN:
                        {
                            return first.Contains(second);
                        }
                    case Operator.NOT_CONTAIN:
                        {
                            return !first.Contains(second);
                        }
                    case Operator.START_WITH:
                        {
                            return first.StartsWith(second);
                        }
                    case Operator.ENDS_WITH:
                        {
                            return first.EndsWith(second);
                        }
                }
            }
            return false;
        }
        public override string ToString()
        {
            return base.getString("Type") + ' ' + base.getString("Attribute") + ' ' + operatorText[(int)base.getNumber("Operator")] + ' ' + base.getString("Value");
        }
        public override string getType()
        {
            return new Serializable.Object().getType();
        }
    }
}
