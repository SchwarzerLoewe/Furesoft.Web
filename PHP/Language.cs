using Furesoft.Web;
using Mono.Net;
using System.IO;

namespace PHP
{
    public class Language : IScriptLanguage
    {
        public override string Name
        {
            get
            {
                return "PHP";
            }
        }

        public override string Extension
        {
            get
            {
                return ".php";
            }
        }

        public override void Execute(string src, HttpListenerContext p, WebConfig wc, StreamWriter sw)
        {
        }
    }
}