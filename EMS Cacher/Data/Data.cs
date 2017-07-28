using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XML;
using JSON;

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
                return obj != null && obj is Boolean && ((Boolean)obj).get() == m_value;
            }
            public override DataType clone()
            {
                return new Serializable.Boolean(m_value);
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
                return obj != null && obj is Number && ((Number)obj).get() == m_value;
            }
            public override DataType clone()
            {
                return new Serializable.Number(m_value);
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
                return obj != null && obj is String && ((String)obj).get() == m_value;
            }
            public override DataType clone()
            {
                return new Serializable.String(m_value);
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
            public virtual Array add(bool value)
            {
                this.add(new Serializable.Boolean(value));
                return this;
            }
            public virtual Array add(double value)
            {
                this.add(new Serializable.Number(value));
                return this;
            }
            public virtual Array add(string value)
            {
                this.add(new Serializable.String(value));
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
                if (!(obj is Array))
                {
                    throw new InvalidCastException("Cannot apply type " + obj.getType() + " on type " + getType());
                }
                m_children.AddRange(obj.getChildren());
                return this;
            }
            public override bool equal(DataType obj)
            {
                if (obj == null || !(obj is Array) || ((Array)obj).size() != this.size())
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
            public override DataType clone()
            {
                Serializable.Array array = new Serializable.Array();
                foreach (var i in m_children)
                {
                    array.add(i.Item2.clone());
                }
                return array;
            }
        }
        public class Object : DataType.Object
        {
            protected List<Tuple<string, DataType>> m_children = new List<Tuple<string, DataType>>();
            
            public override List<Tuple<string, DataType>> getChildren()
            {
                return this.m_children;
            }
            public virtual List<string> keys()
            {
                List<string> keys = new List<string>(m_children.Count);
                for (int i = 0; i != m_children.Count; i++)
                {
                    keys.Add(m_children[i].Item1);
                }
                return keys;
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
            public virtual Serializable.Object set(string key, bool value)
            {
                this.set(key, new Serializable.Boolean(value));
                return this;
            }
            public virtual Serializable.Object set(string key, double value)
            {
                this.set(key, new Serializable.Number(value));
                return this;
            }
            public virtual Serializable.Object set(string key, string value)
            {
                this.set(key, new Serializable.String(value));
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
                return this.get(key) != null && this.get(key) is Boolean ?
                    ((Boolean)this.get(key)).get() : false;
            }
            public virtual double getNumber(string key)
            {
                return this.get(key) != null && this.get(key) is Number ?
                    ((Number)this.get(key)).get() : Double.NaN;
            }
            public virtual string getString(string key)
            {
                return this.get(key) != null && this.get(key) is String ?
                    ((String)this.get(key)).get() : null;
            }
            public virtual Serializable.Array getArray(string key)
            {
                return this.get(key) != null && this.get(key) is Array ?
                    ((Array)this.get(key)) : null;
            }
            public virtual Serializable.Object getObject(string key)
            {
                return this.get(key) != null && this.get(key) is Serializable.Object ?
                    ((Serializable.Object)this.get(key)) : null;
            }
            public override DataType.Object apply(DataType.Object obj)
            {
                if (!(obj is Serializable.Object))
                {
                    throw new InvalidCastException("Cannot apply type " + obj.getType() + " on type " + getType());
                }
                List<Tuple<string, DataType>> children = obj.getChildren();
                foreach(Tuple<string, DataType> child in children)
                {
                    if (child.Item2 is DataType.Object &&
                        (this.getObject(child.Item1) != null || this.getArray(child.Item1) != null))
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
                for (int i = this.m_children.Count - 1; i >= 0; i--)
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
                    genericObj is Serializable.Object ||
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
            public override DataType clone()
            {
                Serializable.Object obj = new Serializable.Object();
                foreach (var i in m_children)
                {
                    obj.set(i.Item1, i.Item2.clone());
                }
                return obj;
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
            public abstract Serializable.DataType clone();
            public abstract List<Tuple<string, DataType>> getChildren();
        }
    }
    public static class Transformations
    {
        public static XMLDocument toXML(Serializable.DataType obj)
        {
            return new XMLDocument(new XMLProlog().
                setAttribute("version", "1.0")
                .setAttribute("encoding", "utf-8"),
                _toXML(obj)
            );
        }
        public static Serializable.DataType fromXML(XMLDocument obj)
        {
            return _fromXML(new List<XMLElement> { obj.root() })[0];
        }

        private static XMLElement _toXML(Serializable.DataType var)
        {
            if (var is Serializable.DataType.Primitive)
            {
                return new XMLElement(var.getType()).text(var.getValue());
            }
            else if (var is Serializable.Array)
            {
                XMLElement root = new XMLElement(var.getType());
                foreach (Tuple<string, Serializable.DataType> children in var.getChildren())
                {
                    root.append(_toXML(children.Item2));
                }
                return root;
            }
            else if (var is Serializable.Object)
            {
                XMLElement root = new XMLElement(var.getType());
                foreach (Tuple<string, Serializable.DataType> children in var.getChildren())
                {
                    XMLElement child = _toXML(children.Item2);
                    root.append(child.setAttribute("name", children.Item1));
                }
                return root;
            }
            return null;
        }
        private static List<Serializable.DataType> _fromXML(List<XMLElement> objs)
        {
            List<Serializable.DataType> parsedObjs = new List<Serializable.DataType>();
            foreach (var obj in objs)
            {
                Serializable.DataType parsedObj = null;
                string type = obj.tagName();
                if (type == new Serializable.Boolean().getType())
                {
                    parsedObj = new Serializable.Boolean(Boolean.Parse(obj.text()));
                }
                else if (type == new Serializable.Number().getType())
                {
                    parsedObj = new Serializable.Number(Int32.Parse(obj.text()));
                }
                else if (type == new Serializable.String().getType())
                {
                    parsedObj = new Serializable.String(obj.text());
                }
                else if (type == new Serializable.Array().getType())
                {
                    parsedObj = new Serializable.Array();
                    List<Serializable.DataType> children = _fromXML(obj.children());
                    foreach (Serializable.DataType child in children)
                    {
                        ((Serializable.Array)parsedObj).add(child);
                    }
                }
                else if (type == new Serializable.Object().getType())
                {
                    parsedObj = new Serializable.Object();
                    List<XMLElement> xmlChildren = obj.children();
                    List<Serializable.DataType> children = _fromXML(xmlChildren);
                    for (int i = 0; i != children.Count; i++)
                    {
                        ((Serializable.Object)parsedObj).set(xmlChildren[i].getAttribute("name"), children[i]);
                    }
                }
                if (parsedObj != null)
                {
                    parsedObjs.Add(parsedObj);
                }
            }
            return parsedObjs;
        }

        public static JSONValue toJSON(Serializable.DataType var)
        {
            if (var is Serializable.DataType.Primitive)
            {
                return new JSONValue(var.getType(), var.getValue());
            }
            else if (var is Serializable.Array)
            {
                JSONArray array = new JSONArray();
                foreach (Tuple<string, Serializable.DataType> children in var.getChildren())
                {
                    array.add(toJSON(children.Item2));
                }
                return array;
            }
            else if (var is Serializable.Object)
            {
                JSONObject root = new JSONObject();
                foreach (Tuple<string, Serializable.DataType> children in var.getChildren())
                {
                    root.set(children.Item1, toJSON(children.Item2));
                }
                return root;
            }
            return null;
        }
    }
}
