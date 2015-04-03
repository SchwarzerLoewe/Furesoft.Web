using System;
using Furesoft.Web;
using Mono.Net;
using System.IO;

namespace Ruby
{
    public class Language : IScriptLanguage
    {
        public override void Execute(string src, Uri uri, HttpListenerContext p, WebConfig wc, StreamWriter sw)
        {
            Microsoft.Scripting.Hosting.ScriptEngine py = IronRuby.Ruby.CreateEngine();
            Microsoft.Scripting.Hosting.ScriptScope s = py.CreateScope();

            var sapi = new StandardScriptApi(uri, p, sw);
            foreach (var item in sapi.Variables)
            {
                s.SetVariable(item.Key, item.Value);
            }

            py.Execute(src, s);
        }

        public override string Name
        {
            get
            {
                return "Ruby";
            }
        }

        public override string Extension
        {
            get
            {
                return ".rb";
            }
        }

    }
}