using System.IO;
using System.Net;

namespace Furesoft.Web.ScriptingTypes
{
    public class XmlHttpRequest
    {
        private int readyState;
        private HttpWebRequest webrequest;
        public dynamic OnReadyStateChange;

        public int ReadyState
        {
            get { return readyState; }
            set
            {
                if (OnReadyStateChange != null)
                    OnReadyStateChange();
                readyState = value;
            }
        }

        public string Mimetype
        {
            get { return webrequest.MediaType; }
            set { webrequest.MediaType = value; }
        }

        public string Responsetext { get; set; }

        public void Open(string method, string url)
        {
            webrequest = (HttpWebRequest)WebRequest.Create(url);
            webrequest.Method = method;
            ReadyState = 0;
        }

        public void Send(object data)
        {
            if (data == null)
            {
                var resp = (HttpWebResponse)webrequest.GetResponse();
                using (Stream s = resp.GetResponseStream())
                {
                    Responsetext = new StreamReader(s).ReadToEnd();
                    ReadyState = 1;
                }
            }
            else
            {
                using (Stream s = webrequest.GetRequestStream())
                {
                    var sw = new StreamWriter(s);
                    sw.Write(data);
                    sw.Flush();
                    ReadyState = 2;
                }
            }
        }
    }
}