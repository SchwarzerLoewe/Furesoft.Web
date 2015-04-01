using Mono.Net;
using System;
using System.IO;
using System.Xml;

namespace Furesoft.Web.Internal
{
    public class TagMatcher
    {
        public void Match(string src, Func<FileInfo, IScriptLanguage> Get, HttpListenerContext p, WebConfig wc, StreamWriter sw)
        {
            var doc = new XmlDocument();
            doc.LoadXml(src);

            var scripts = doc.GetElementsByTagName("script");
            foreach (XmlNode s in scripts)
            {
                var engine = Get(new FileInfo("." + s.Attributes["language"]));
                //engine.Execute(s.InnerText, p, wc, sw);
            }
        }
    }
}