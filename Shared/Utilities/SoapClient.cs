using System;
using System.Net;
using System.Collections.Generic;
using XML;
using HTTP;

namespace Soap
{
    public class SoapClient
    {
        protected string url = string.Empty;
        protected XMLDocument root = null;
        protected XMLElement body = null;
        private HTTPClient request = null;
        
        public SoapClient()
        {
            this.body = new XMLElement("soap12:Body");
            this.root = new XMLDocument(
                new XMLProlog()
                    .setAttribute("version", "1.0")
                    .setAttribute("encoding", "utf-8"),
                new XMLElement("soap12:Envelope")
                    .setAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
                    .setAttribute("xmlns:xsd", "http://www.w3.org/2001/XMLSchema")
                    .setAttribute("xmlns:soap12", "http://www.w3.org/2003/05/soap-envelope")
                    .append(this.body)
            );
        }
        public SoapClient setURL(string url)
        {
            this.url = url;
            return this;
        }
        public SoapClient setRequest(List<XMLElement> parameters)
        {
            foreach (XMLElement i in parameters)
            {
                this.body.append(i);
            }
            return this;
        }
        public SoapClient send()
        {
            this.request = new HTTPClient("POST", this.url)
                .setHeader("Content-Type", "application/soap+xml; charset=utf-8")
                .send(this.root.ToString());
            return this;
        }
        public string getRequest()
        {
            if (request == null) {
                throw new WebException("The request has not been sent. Did you forget to call send()?");
            }
            return request.getRequest();
        }
        public XMLDocument getResponse()
        {
            if (request == null)
            {
                throw new WebException("The request has not been sent. Did you forget to call send()?");
            }
            return XMLDocument.inflate(request.getResponse());
        }
    }
}