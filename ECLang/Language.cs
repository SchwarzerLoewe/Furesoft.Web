using Furesoft.Web;
using Mono.Net;
using System.IO;

namespace ECLang
{
    public class Language : IScriptLanguage
    {
        public override string Name
        {
            get
            {
                return "ECLang";
            }
        }

        public override string Extension
        {
            get
            {
                return ".ec";
            }
        }

        public override void Execute(string src, HttpListenerContext p, WebConfig wc, StreamWriter sw)
        {
            var en = new ECLang.Engine();
            var sapi = new StandardScriptApi(p, sw);

            foreach (var item in sapi.Functions)
            {
                en.AddItem(item.Key, item.Value);
            }
            foreach (var item in sapi.Variables)
            {
                en.AddItem(item.Key, item.Value);
            }
            foreach (var item in sapi.Types)
            {
                en.AddItem(item.Key, item.Value);
            }

            en.Execute(src);
        }
    }
}