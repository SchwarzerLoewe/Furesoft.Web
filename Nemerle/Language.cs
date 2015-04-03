using Furesoft.Web;
using Mono.Net;
using System;
using System.IO;

namespace Nemerle
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
                return "Nemerle";
            }
        }

        public override string Extension
        {
            get
            {
                return ".n";
            }
        }

    }
}