using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSON
{
    public class JSONObject : JSONValue
    {
        protected Dictionary<string, JSONValue> values = new Dictionary<string, JSONValue>();

        public JSONObject() : base(null, "object")
        {
        }
        public JSONObject set(string key, JSONValue value)
        {
            values.Add(key, value);
            return this;
        }
        public override string ToString()
        {
            string total = "";
            for (int i = 0; i != values.Count; i++)
            {
                KeyValuePair<string, JSONValue> pair = values.ElementAt(i);
                total += '"' + JSONValue.encodeString(pair.Key) + "\": " + pair.Value.ToString();
                if (i < values.Count - 1)
                {
                    total += "," + Environment.NewLine;
                }
            }
            string[] split = total.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            for (int i = 0; i < split.Length; i++)
            {
                split[i] = "  " + split[i];
            }
            total = string.Join(Environment.NewLine, split);
            return '{' + Environment.NewLine
                + total + Environment.NewLine
                + '}';
        }
        public Dictionary<string, JSONValue> children()
        {
            return values;
        }
    }
    public class JSONArray : JSONValue
    {
        protected List<JSONValue> values = new List<JSONValue>();

        public JSONArray() : base(null, "array")
        {
        }
        public JSONArray add(JSONValue value)
        {
            values.Add(value);
            return this;
        }
        public override string ToString()
        {
            string total = "";
            for (int i = 0; i != values.Count; i++)
            {
                total += values.ElementAt(i).ToString();
                if (i < values.Count - 1)
                {
                    total += "," + Environment.NewLine;
                }
            }
            string[] split = total.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            for (int i = 1; i < split.Length - 1; i++)
            {
                split[i] = "  " + split[i];
            }
            total = string.Join(Environment.NewLine, split);
            return '[' + total + ']';
        }
        public List<JSONValue> children()
        {
            return values;
        }
    }
    public class JSONValue
    {
        protected static Dictionary<string, string> escapeChars = new Dictionary<string, string>()
        {
            { "\"", "\\\"" },
            { "\n", "\\n" },
            { "\r", "\\r" },
            { "\t", "\\t" },
            { "\b", "\\b" },
            { "\f", "\\f" },
            { "\\", "\\\\" }
        };
        protected static string encodeString(string str)
        {
            foreach (var escapeChar in escapeChars.Reverse())
            {
                str = str.Replace(escapeChar.Key, escapeChar.Value);
            }
            return str;
        }
        protected static string decodeEscape(string str)
        {
            foreach (var escapeChar in escapeChars)
            {
                str = str.Replace(escapeChar.Value, escapeChar.Key);
            }
            return str;
        }

        protected string m_value;
        protected string m_type;

        public JSONValue(string type, string value)
        {
            if (type != null && type.ToLower() == "string")
            {
                value = '"' + JSONValue.encodeString(value) + '"';
            }
            this.m_value = value;
            this.m_type = type;
        }
        public override string ToString()
        {
            return m_value;
        }
        public virtual string dataType()
        {
            return m_type;
        }
    }
}
