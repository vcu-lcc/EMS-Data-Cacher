using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using static Persistence;

namespace HTTP
{
    class HTTPClient
    {
        protected HttpWebRequest request = null;
        protected Dictionary<string, string> headers = new Dictionary<string, string>()
        {
            {"Connection", "Keep-Alive"}
        };
        protected string method = "";
        protected string path = "";
        protected string requestBody = "";
        protected string url = "";

        public HTTPClient(string method, string url)
        {
            this.method = method;
            url = url.Trim();
            this.url = url;
            string host = string.Empty;
            if (url.StartsWith("https://"))
            {
                host = url.Substring(8, url.IndexOf('/', 8) == -1 ? url.Length - 8 : url.IndexOf('/', 8) - 8);
                this.path = url.Substring(url.IndexOf('/', 9) == -1 ? url.Length : url.IndexOf('/', 9));
            }
            else if (url.StartsWith("http://"))
            {
                host = url.Substring(7, url.IndexOf('/', 7) == -1 ? url.Length - 7 : url.IndexOf('/', 7) - 7);
                this.path = url.Substring(url.IndexOf('/', 8) == -1 ? url.Length : url.IndexOf('/', 8));
            }
            else
            {
                host = url.Substring(0, url.IndexOf('/') == -1 ? url.Length : url.IndexOf('/'));
                this.path = url.Substring(url.IndexOf('/') == -1 ? url.Length : url.IndexOf('/'));
            }
            this.headers.Add("Host", host);
            this.request = (HttpWebRequest) WebRequest.Create(url);
            this.request.Method = method;
            this.request.ServicePoint.Expect100Continue = false;
        }
        public HTTPClient setHeader(string key, string value)
        {
            switch (key)
            {
                case "Content-Length":
                    this.request.ContentLength = Int32.Parse(value);
                    break;
                case "Content-Type":
                    this.request.ContentType = value;
                    break;
                default:
                    this.request.Headers.Add(key, value);
                    break;
            }
            headers.Add(key, value);
            return this;
        }
        public HTTPClient send(string value)
        {
            this.requestBody = value;
            byte[] data = Encoding.UTF8.GetBytes(value);
            if (!this.headers.ContainsKey("Content-Length"))
            {
                this.setHeader("Content-Length", data.Length.ToString());
            }
            try
            {
                var requestStream = this.request.GetRequestStream();
                requestStream.Write(data, 0, data.Length);
                requestStream.Close();
            }
            catch (WebException e)
            {
                console.error(e, "Unable to connect to " + this.url);
            }
            return this;
        }
        public HTTPClient send()
        {
            return this;
        }
        public string getResponse()
        {
            try
            {
                console.verbose(string.Empty, "Sending request: ", getRequest(), string.Empty);
                return new StreamReader(this.request.GetResponse().GetResponseStream()).ReadToEnd();
            }
            catch (ProtocolViolationException e)
            {
                console.error(e, "A protocol violation occured. This may be because that you didn't call send().");
            }
            catch (WebException e)
            {
                console.error(e, "A WebException occured.");
            }
            return string.Empty;
        }
        public string getRequest()
        {
            string total = this.method + (this.path.Length > 0 ? " " + this.path : "") + " HTTP/1.1"
                + Environment.NewLine;
            foreach (var i in headers)
            {
                total += i.Key + ": " + i.Value + Environment.NewLine;
            }
            if (requestBody.Length > 0)
            {
                total += Environment.NewLine + requestBody;
            }
            return total;
        }
    }
}
