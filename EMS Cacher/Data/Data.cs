using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XML;
using JSON;
using EducationalInstitution;
using static Persistence;

namespace Data
{
    public static class Serializable
    {
        public class Boolean : DataType.Primitive
        {
            protected bool m_value = false;

            public Boolean()
            {
                this.m_value = false;
            }
            public Boolean(bool value)
            {
                this.m_value = value;
            }
            public override string getValue()
            {
                return this.m_value ? "true" : "false";
            }
            public override string getType()
            {
                return "boolean";
            }
            public bool get()
            {
                return this.m_value;
            }
            public Boolean setValue(bool value)
            {
                this.m_value = value;
                return this;
            }
            public override bool equal(DataType obj)
            {
                return obj != null && obj.getType() == "boolean" && ((Serializable.Boolean)obj).get() == m_value;
            }
        }
        public class Number : DataType.Primitive
        {
            protected double m_value = 0;

            public Number()
            {
                this.m_value = 0;
            }
            public Number(double num)
            {
                this.m_value = num;
            }
            public override string getValue()
            {
                return this.m_value.ToString();
            }
            public double get()
            {
                return this.m_value;
            }
            public override string getType()
            {
                return "number";
            }
            public Number setValue(double value)
            {
                this.m_value = value;
                return this;
            }
            public override bool equal(DataType obj)
            {
                return obj != null && obj.getType() == "number" && ((Serializable.Number)obj).get() == m_value;
            }
        }
        public class String : DataType.Primitive
        {
            protected string m_value = string.Empty;

            public String()
            {
                this.m_value = "";
            }
            public String(string value)
            {
                this.m_value = value;
            }
            public override string getValue()
            {
                return this.m_value;
            }
            public string get()
            {
                return this.m_value;
            }
            public override string getType()
            {
                return "string";
            }
            public String setValue(string value)
            {
                this.m_value = value;
                return this;
            }
            public override bool equal(DataType obj)
            {
                if (obj == null && m_value == null)
                {
                    return true;
                }
                return obj != null && obj.getType() == "string" && ((Serializable.String)obj).get() == m_value;
            }
        }
        public class Array : DataType.Object
        {
            protected List<Tuple<string, DataType>> m_children = new List<Tuple<string, DataType>>();

            public override List<Tuple<string, DataType>> getChildren()
            {
                return this.m_children;
            }
            public virtual Array add(DataType value)
            {
                this.m_children.Add(new Tuple<string, DataType>(null, value));
                return this;
            }
            public virtual Array removeAt(int i)
            {
                this.m_children.RemoveAt(i);
                return this;
            }
            public virtual DataType get(int i)
            {
                return m_children[i].Item2;
            }
            public virtual List<DataType> toArray()
            {
                List<DataType> children = new List<DataType>();
                foreach (var i in m_children)
                {
                    children.Add(i.Item2);
                }
                return children;
            }
            public override string getType()
            {
                return "array";
            }
            public virtual int size()
            {
                return m_children.Count();
            }
            public override DataType.Object apply(DataType.Object obj)
            {
                if (obj.getType() != "array")
                {
                    throw new InvalidCastException("Cannot apply type " + obj.getType() + " on type " + getType());
                }
                m_children.AddRange(obj.getChildren());
                return this;
            }
            public override bool equal(DataType obj)
            {
                if (obj == null || obj.getType() != "array" || ((Serializable.Array)obj).size() != this.size())
                {
                    return false;
                }
                List<Tuple<string, DataType>> children = ((Serializable.Array)obj).getChildren();
                for (int i = 0; i != children.Count; i++)
                {
                    if (!children[i].Item2.equal(m_children[i].Item2))
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        public class Object : DataType.Object
        {
            protected List<Tuple<string, DataType>> m_children = new List<Tuple<string, DataType>>();
            
            public override List<Tuple<string, DataType>> getChildren()
            {
                return this.m_children;
            }
            public virtual Serializable.Object set(string key, DataType value)
            {
                for (int i = 0; i != this.m_children.Count; i++)
                {
                    if (this.m_children[i].Item1 == key)
                    {
                        this.m_children[i] = new Tuple<string, DataType>(key, value);
                        return this;
                    }
                }
                this.m_children.Add(new Tuple<string, DataType>(key, value));
                return this;
            }
            public virtual DataType get(string key)
            {
                foreach (var i in m_children)
                {
                    if (i.Item1 == key)
                    {
                        return i.Item2;
                    }
                }
                return null;
            }
            public virtual string getType(string key)
            {
                DataType data = this.get(key);
                return data == null ? "undefined" : data.getType();
            }
            public virtual bool getBoolean(string key)
            {
                return this.get(key) != null && this.get(key).getType() == "boolean" ?
                    ((Serializable.Boolean)this.get(key)).get() : false;
            }
            public virtual double getNumber(string key)
            {
                return this.get(key) != null && this.get(key).getType() == "number" ?
                    ((Serializable.Number)this.get(key)).get() : Double.NaN;
            }
            public virtual string getString(string key)
            {
                return this.get(key) != null && this.get(key).getType() == "string" ?
                    ((Serializable.String)this.get(key)).get() : null;
            }
            public virtual Serializable.Array getArray(string key)
            {
                return this.get(key) != null && this.get(key).getType() == "array" ?
                    ((Serializable.Array)this.get(key)) : null;
            }
            public virtual Serializable.Object getObject(string key)
            {
                return this.get(key) != null && this.get(key).getType() == "object" ?
                    ((Serializable.Object)this.get(key)) : null;
            }
            public override DataType.Object apply(DataType.Object obj)
            {
                if (obj.getType() != "object")
                {
                    throw new InvalidCastException("Cannot apply type " + obj.getType() + " on type " + getType());
                }
                List<Tuple<string, DataType>> children = obj.getChildren();
                foreach(Tuple<string, DataType> child in children)
                {
                    if (child.Item2.getType() == "object" && this.getObject(child.Item1) != null
                        || child.Item2.getType() == "array" && this.getArray(child.Item1) != null
                        )
                    {
                        ((DataType.Object)this.get(child.Item1)).apply((DataType.Object)child.Item2);
                    }
                    else
                    {
                        this.set(child.Item1, child.Item2);
                    }
                }
                return this;
            }
            public virtual Serializable.Object remove(string key)
            {
                for (int i = this.m_children.Count - 1; i >= 0; i++)
                {
                    if (this.m_children[i].Item1 == key)
                    {
                        this.m_children.RemoveAt(i);
                    }
                }
                return this;
            }
            public override bool equal(DataType genericObj)
            {
                if (genericObj == null ||
                    genericObj.getType() != "object" ||
                    genericObj.getChildren().Count != m_children.Count)
                {
                    return false;
                }
                Serializable.Object obj = (Serializable.Object)genericObj;
                foreach (Tuple<string, DataType> i in m_children)
                {
                    if (obj.get(i.Item1) == null ^ i.Item2 == null)
                    {
                        return false;
                    }
                    else if (!obj.get(i.Item1).equal(i.Item2))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public abstract class DataType
        {
            public abstract class Primitive : DataType
            {
                public override List<Tuple<string, DataType>> getChildren()
                {
                    return null;
                }
            }
            public abstract class Object : DataType
            {
                public abstract DataType.Object apply(DataType.Object obj);
                public override string getValue()
                {
                    return null;
                }
                public override string getType()
                {
                    return "object";
                }
            }

            public abstract string getType();
            public abstract string getValue();
            public abstract bool equal(DataType obj);
            public abstract List<Tuple<string, DataType>> getChildren();
        }
    }
    public static class Transformations
    {
        public static XMLDocument toXML(Serializable.DataType obj)
        {
            return new XMLDocument(_toXML(obj));
        }
        public static Serializable.DataType fromXML(XMLDocument obj)
        {
            return _fromXML(new List<XMLElement> { obj.root() })[0];
        }

        private static XMLElement _toXML(Serializable.DataType var)
        {
            switch (var.getType())
            {
                case "boolean":
                case "number":
                case "string":
                    {
                        return new XMLElement(var.getType()).text(var.getValue());
                    }
                case "array":
                    {
                        XMLElement root = new XMLElement(var.getType());
                        foreach (Tuple<string, Serializable.DataType> children in var.getChildren())
                        {
                            root.append(_toXML(children.Item2));
                        }
                        return root;
                    }
                case "object":
                default:
                    {
                        XMLElement root = new XMLElement(var.getType());
                        foreach (Tuple<string, Serializable.DataType> children in var.getChildren())
                        {
                            XMLElement child = _toXML(children.Item2);
                            root.append(child.setAttribute("name", children.Item1));
                        }
                        return root;
                    }
            }
        }
        private static List<Serializable.DataType> _fromXML(List<XMLElement> objs)
        {
            List<Serializable.DataType> parsedObjs = new List<Serializable.DataType>();
            foreach (var obj in objs)
            {
                Serializable.DataType parsedObj = null;
                switch (obj.tagName())
                {
                    case "boolean":
                        parsedObj = new Serializable.Boolean(Boolean.Parse(obj.text()));
                        break;
                    case "number":
                        parsedObj = new Serializable.Number(Int32.Parse(obj.text()));
                        break;
                    case "string":
                        parsedObj = new Serializable.String(obj.text());
                        break;
                    case "array":
                        {
                            parsedObj = new Serializable.Array();
                            List<Serializable.DataType> children = _fromXML(obj.children());
                            foreach (Serializable.DataType child in children)
                            {
                                ((Serializable.Array)parsedObj).add(child);
                            }
                            break;
                        }
                    case "object":
                        {
                            parsedObj = new Serializable.Object();
                            List<XMLElement> xmlChildren = obj.children();
                            List<Serializable.DataType> children = _fromXML(xmlChildren);
                            for (int i = 0; i != children.Count; i++)
                            {
                                ((Serializable.Object)parsedObj).set(xmlChildren[i].getAttribute("name"), children[i]);
                            }
                            break;
                        }
                    case "comment":
                        {
                            break;
                        }
                    default:
                        console.warn("Unknown XML Tag " + obj.tagName());
                        break;
                }
                parsedObjs.Add(parsedObj);
            }
            return parsedObjs;
        }

        public static JSONValue toJSON(Serializable.DataType var)
        {
            switch (var.getType())
            {
                case "boolean":
                case "number":
                case "string":
                    {
                        return new JSONValue(var.getType(), var.getValue());
                    }
                case "array":
                    {
                        JSONArray array = new JSONArray();
                        foreach (Tuple<string, Serializable.DataType> children in var.getChildren())
                        {
                            array.add(toJSON(children.Item2));
                        }
                        return array;
                    }
                case "object":
                default:
                    {
                        JSONObject root = new JSONObject();
                        foreach (Tuple<string, Serializable.DataType> children in var.getChildren())
                        {
                            root.set(children.Item1, toJSON(children.Item2));
                        }
                        return root;
                    }
            }
        }
    }
}
