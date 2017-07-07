using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace XML
{
    public class XMLDocument : XMLNode
    {
        public static XMLDocument inflate(string xml)
        {
            XMLProlog prolog = null;
            List<XMLElement> root = null;
            xml = xml.Trim();
            bool str = false;
            for (int i = 0; i != xml.Length; i++)
            {
                if (xml[i] == '"')
                {
                    str = !str;
                }
                else if (prolog == null && !str && xml[i] == '>')
                {
                    prolog = XMLProlog.inflate(xml.Substring(0, i + 1));
                }
                else if (prolog != null)
                {
                    root = XMLElement.inflate(xml.Substring(i, xml.Length - i));
                    break;
                }
            }
            if (root == null || root.Count == 0)
            {
                return null;
            }
            if (prolog == null)
            {
                return new XMLDocument(root[0]);
            }
            return new XMLDocument(prolog, root[0]);
        }

        protected XMLProlog m_prolog;
        protected XMLElement m_rootNode;

        public XMLDocument(XMLProlog prolog, XMLElement root) : base(string.Empty)
        {
            this.m_prolog = prolog;
            this.m_rootNode = root;
        }
        public XMLDocument(XMLElement root) : base(string.Empty)
        {
            this.m_prolog = new XMLProlog();
            this.m_rootNode = root;
        }
        public XMLElement root()
        {
            return m_rootNode;
        }
        public XMLProlog prolog()
        {
            return m_prolog;
        }
        public override string innerXML()
        {
            return m_prolog.outerXML() + Environment.NewLine + m_rootNode.outerXML();
        }
        public override string outerXML()
        {
            return this.innerXML();
        }
        public override string ToString()
        {
            return this.outerXML();
        }
    }
    public class XMLProlog : XMLNode
    {
        public static XMLProlog inflate(string prolog)
        {
            string open = null;
            string tagName = null;
            string close = null;
            List<string> inlineAttributes = new List<string>();
            bool str = false;
            for (int i = 0, lastIndex = 0; i != prolog.Length; i++)
            {
                if (open == null)
                {
                    if ('a' < prolog[i] && prolog[i] < 'z' || 'A' < prolog[i] && prolog[i] < 'Z')
                    {
                        open = prolog.Substring(0, i);
                        lastIndex = i;
                    }
                }
                else if (tagName == null)
                {
                    if (prolog[i] == ' ' || prolog[i] == '\n' || prolog[i] == '\t')
                    {
                        tagName = prolog.Substring(lastIndex, i - lastIndex);
                        lastIndex = i + 1;
                    }
                }
                else if (close == null)
                {
                    if (prolog[i] == '"')
                    {
                        if (str)
                        {
                            inlineAttributes.Add(prolog.Substring(lastIndex, i - lastIndex + 1));
                            lastIndex = i + 1;
                        }
                        str = !str;
                    }
                    else if (!str && prolog[i] == '>')
                    {
                        close = prolog.Substring(lastIndex, i - lastIndex + 1);
                    }
                    else if (!str && (prolog[i] == ' ' || prolog[i] == '\t' || prolog[i] == '\n'))
                    {
                        lastIndex = i + 1;
                    }
                }
            }
            XMLProlog generatedProlog = new XMLProlog(open, tagName, close);
            foreach (string attribute in inlineAttributes)
            {
                int delim = attribute.IndexOf('=');
                string attributeName = attribute.Substring(0, delim);
                string attributeValue = attribute.Substring(delim + 2, attribute.Length - delim - 3);
                generatedProlog.setAttribute(attributeName, XMLNode.decodeEscape(attributeValue));
            }
            return generatedProlog;
        }

        protected string m_open = "<?";
        protected string m_close = "?>";
        protected Dictionary<string, string> m_attributes = new Dictionary<string, string>();

        public XMLProlog() : base("xml")
        {
        }
        public XMLProlog(string tagName) : base(tagName)
        {
        }
        public XMLProlog(string before, string tagName, string after) : base(tagName)
        {
            this.m_open = before;
            this.m_close = after;
        }
        public XMLProlog setAttribute(string key, string value)
        {
            m_attributes.Add(key, XMLNode.encodeEscape(value));
            return this;
        }
        public string getAttribute(string key)
        {
            return XMLNode.decodeEscape(this.m_attributes[key]);
        }
        public override string outerXML()
        {
            string total = m_open + m_tagName;
            foreach (var i in m_attributes)
            {
                total += ' ' + i.Key + "=\"" + XMLNode.encodeEscape(i.Value) + '"';
            }
            total += m_close;
            return total;
        }
        public override string innerXML()
        {
            return string.Empty;
        }
    }
    public class XMLElement : XMLNode
    {
        public static List<XMLElement> inflate(string outerXML)
        {
            outerXML = outerXML.Trim();
            List<XMLElement> elements = new List<XMLElement>();
            XMLElement currentElement = null;
            bool startPhrase = false;
            int index = 0;
            bool quot = false;
            for (int i = 0; i != outerXML.Length; i++)
            {
                if (outerXML[i] == '"')
                {
                    quot = !quot;
                }
                else if (!startPhrase && isAlphabetic(outerXML[i]))
                {
                    startPhrase = true;
                    index = i;
                }
                else if (currentElement == null)
                {
                    if (isDeliminator(outerXML[i]))
                    {
                        currentElement = new XMLElement(outerXML.Substring(index, i - index));
                        startPhrase = false;
                    }
                }
                else if (!quot)
                {
                    if (startPhrase && isDeliminator(outerXML[i]))
                    {
                        string rawAttribute = outerXML.Substring(index, i - index);
                        int splitPos = rawAttribute.IndexOf('=');
                        if (splitPos == -1)
                        {
                            currentElement.setAttribute(rawAttribute, "");
                        }
                        else
                        {
                            currentElement.setAttribute(
                                rawAttribute.Substring(0, splitPos),
                                decodeEscape(rawAttribute.Substring(splitPos + 2, rawAttribute.Length - splitPos - 3).Trim())
                            );
                        }
                        startPhrase = false;
                    }
                }
                if (!quot && XMLNode.startsAfterWhiteSpace(outerXML.Substring(i), "/>"))
                {
                    elements.Add(currentElement);
                    currentElement.selfClosing(true);
                    i = outerXML.IndexOf('<', i + 2);
                    if (i == -1)
                    {
                        break;
                    }
                    currentElement = null;
                    startPhrase = false;
                    index = i;
                    quot = false;
                }
                else if (!quot && outerXML[i] == '>')
                {
                    elements.Add(currentElement);
                    int closeTagIndex = i + indexOfClosingTag(outerXML.Substring(i), currentElement.tagName());
                    if (closeTagIndex < i)
                    {
                        throw new Exception("Malformed XML");
                    }
                    string innerXML = outerXML.Substring(i + 1, closeTagIndex - i - 1).Trim();
                    if (innerXML.StartsWith("<") && innerXML.EndsWith(">"))
                    {
                        List<XMLElement> children = inflate(innerXML);
                        foreach (XMLElement child in children)
                        {
                            currentElement.append(child);
                        }
                    }
                    else
                    {
                        currentElement.text(decodeEscape(innerXML));
                    }
                    i = outerXML.IndexOf('<', closeTagIndex + 1);
                    if (i == -1)
                    {
                        break;
                    }
                    currentElement = null;
                    startPhrase = false;
                    index = i;
                    quot = false;
                }
            }
            return elements;
        }

        protected List<XMLElement> m_children = new List<XMLElement>();
        protected string m_innerText = string.Empty;
        protected bool m_selfClosing = false;
        protected Dictionary<string, string> m_attributes = new Dictionary<string, string>();

        public XMLElement(string tagName) : base(tagName)
        {
        }
        public List<XMLElement> children()
        {
            return m_children;
        }
        public List<XMLElement> getElementsByTagName(string name)
        {
            List<XMLElement> elems = new List<XMLElement>();
            if (m_tagName == name)
            {
                elems.Add(this);
            }
            foreach (XMLElement i in m_children)
            {
                elems.AddRange(i.getElementsByTagName(name));
            }
            return elems;
        }
        public XMLElement getElementByTagName(string name)
        {
            List<XMLElement> all = this.getElementsByTagName(name);
            return all.Count > 0 ? all[0] : null;
        }
        public List<XMLElement> getElementsByAttribute(string attr, string value)
        {
            List<XMLElement> elems = new List<XMLElement>();
            if (this.getAttribute(attr) == value)
            {
                elems.Add(this);
            }
            foreach (XMLElement i in m_children)
            {
                elems.AddRange(i.getElementsByAttribute(attr, value));
            }
            return elems;
        }
        public XMLElement getElementByAttribute(string attr, string value)
        {
            List<XMLElement> all = this.getElementsByAttribute(attr, value);
            return all.Count > 0 ? all[0] : null;
        }
        public XMLElement selfClosing(bool status)
        {
            if (this.m_selfClosing != status)
            {
                this.m_selfClosing = status;
                if (status)
                {
                    this.m_innerText = string.Empty;
                    this.m_children = new List<XMLElement>();
                }
            }
            return this;
        }
        public bool selfClosing()
        {
            return this.m_selfClosing;
        }
        public XMLElement append(XMLElement child)
        {
            m_children.Add(child);
            this.m_selfClosing = false;
            this.m_innerText = string.Empty;
            return this;
        }
        public XMLElement append(List<XMLElement> children)
        {
            m_children.AddRange(children);
            this.m_selfClosing = false;
            this.m_innerText = string.Empty;
            return this;
        }
        public XMLElement text(string innerText)
        {
            this.m_children = new List<XMLElement>();
            this.m_selfClosing = false;
            this.m_innerText = XMLNode.encodeEscape(innerText);
            return this;
        }
        public XMLElement setAttribute(string key, string value)
        {
            m_attributes.Add(key, XMLNode.encodeEscape(value));
            return this;
        }
        public string getAttribute(string key)
        {
            string value = null;
            this.m_attributes.TryGetValue(key, out value);
            XMLNode.decodeEscape(value);
            return value;
        }
        public string text()
        {
            if (this.m_children.Count == 0)
            {
                if (m_innerText.Length > 1000)
                {
                }
                return XMLNode.decodeEscape(this.m_innerText);
            }
            string total = string.Empty;
            foreach (XMLElement elem in m_children)
            {
                total += elem.text();
            }
            return total;
        }
        public string tagName()
        {
            return m_tagName;
        }
        public override string innerXML()
        {
            if (this.m_children.Count == 0)
            {
                return this.m_innerText;
            }
            string total = "";
            for (int child = 0; child != this.m_children.Count; child++)
            {
                total += this.m_children[child].outerXML() + Environment.NewLine;
            }
            return total;
        }
        public override string outerXML()
        {
            string total = '<' + m_tagName;
            foreach (var i in m_attributes)
            {
                total += ' ' + i.Key + "=\"" + i.Value + '"';
            }
            string[] innerXML = this.innerXML().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            for (int i = 0; i < innerXML.Length - 1; i++)
            {
                innerXML[i] = "  " + innerXML[i];
            }
            if (this.m_selfClosing)
            {
                total += " />";
            }
            else
            {
                total += '>';
                if (this.m_children.Count > 0)
                {
                    total += Environment.NewLine;
                }
                total += string.Join(Environment.NewLine, innerXML) + "</" + this.m_tagName + '>';
            }
            return total;
        }
        public override string ToString()
        {
            return this.outerXML();
        }
    }
    public abstract class XMLNode
    {
        protected string m_tagName;
        protected static Dictionary<string, string> escapeChars = new Dictionary<string, string>()
        {
            {"\"", "&quot;"},
            {"'", "&apos;"},
            {"<", "&lt;"},
            {">", "&gt;"},
            {"&", "&amp;"}
        };

        public XMLNode(string tagName)
        {
            this.m_tagName = tagName;
        }
        public abstract string outerXML();
        public abstract string innerXML();
        protected static string encodeEscape(string str)
        {
            if (str == null)
            {
                return str;
            }
            foreach (var escapeChar in escapeChars.Reverse())
            {
                str = str.Replace(escapeChar.Key, escapeChar.Value);
            }
            return str;
        }
        protected static string decodeEscape(string str)
        {
            if (str == null)
            {
                return str;
            }
            foreach (var escapeChar in escapeChars)
            {
                str = str.Replace(escapeChar.Value, escapeChar.Key);
            }
            return str;
        }
        protected static bool isAlphabetic(char c)
        {
            return 'a' <= c && c <= 'z' || 'A' <= c && c <= 'Z';
        }
        protected static bool isWhiteSpace(char c)
        {
            return c == ' ' || c == '\n' || c == '\t';
        }
        protected static bool isDeliminator(char c)
        {
            return c == ' ' || c == '\n' || c == '>' || c == '\t';
        }
        protected static bool startsAfterWhiteSpace(string str, string sub)
        {
            return str.Trim().IndexOf(sub) == 0;
        }
        protected static int indexOfClosingTag(string s, string tagName) {
            bool str = false;
            bool openBracket = true;
            int counts = 1;
            for (int i = 0; i < s.Length - 1; i++)
            {
                if (openBracket && s[i] == '"')
                {
                    str = !str;
                }
                else if (!str)
                {
                    if (s[i] == '>')
                    {
                        openBracket = false;
                    }
                    if (s[i] == '<' && s[i + 1] == '/')
                    {
                        if (startsAfterWhiteSpace(s.Substring(i + 2), tagName))
                        {
                            if (--counts == 0)
                            {
                                return i;
                            }
                        }
                    }
                    else if (s[i] == '<')
                    {
                        openBracket = true;
                        if (startsAfterWhiteSpace(s.Substring(i + 1), tagName))
                        {
                            counts++;
                        }
                    }
                }
            }
            return -1;
        }
    }
}

