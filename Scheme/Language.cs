using System;
using Furesoft.Web;
using Mono.Net;
using System.IO;

namespace Scheme
{
    public class Language : IScriptLanguage
    {
        public override void Execute(string src, Uri uri, HttpListenerContext p, WebConfig wc, StreamWriter sw)
        {
            var eng = IronScheme.RuntimeExtensions.ScriptEngine.CompileCode(src);
            var mod = eng.MakeModule("web");
            var sapi = new StandardScriptApi(uri, p, sw);

            foreach (var v in sapi.Variables)
            {
                mod.SetVariable(v.Key, v.Value);
            }

            eng.Execute(mod);
        }

        public override string Name
        {
            get
            {
                return "Scheme";
            }
        }

        public override string Extension
        {
            get
            {
                return ".sls";
            }
        }

    }
}