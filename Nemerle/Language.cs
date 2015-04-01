using Furesoft.Web;
using Mono.Net;
using System;
using System.IO;

namespace Nemerle
{
    public class Language : IScriptLanguage
    {
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

        public override void Execute(string src, HttpListenerContext p, WebConfig wc, StreamWriter sw)
        {
            throw new NotImplementedException();
        }
    }
}