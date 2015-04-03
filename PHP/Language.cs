using System;
using Furesoft.Web;
using Mono.Net;
using System.IO;

namespace PHP
{
    public class Language : IScriptLanguage
    {
        public override void Execute(string src, Uri uri, HttpListenerContext p, WebConfig wc, StreamWriter sw)
        {
            
        }

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

    }
}