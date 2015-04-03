using Furesoft.Web;
using Mono.Net;
using System;
using System.IO;

namespace VBScript
{
    public class Language : IScriptLanguage
    {
        public override void Execute(string src, Uri uri, HttpListenerContext p, WebConfig wc, StreamWriter sw)
        {
            VBScript.VBScriptEngine engine = new VBScriptEngine();

            var sapi = new StandardScriptApi(uri, p, sw);

            engine.Add("require", new Action<string>(pa => engine.Execute(File.ReadAllText(pa))));
            engine.Add("eval", new Func<string, object>(pa => engine.Evaluate(pa)));

            foreach (var f in sapi.Functions)
            {
                engine.Add(f.Key, f.Value);
            }
            foreach (var f in sapi.Variables)
            {
                engine.Add(f.Key, f.Value);
            }
            foreach (var f in sapi.Types)
            {
                engine.Add(f.Key, f.Value);
            }
        }

        public override string Name
        {
            get
            {
                return "VBScript";
            }
        }

        public override string Extension
        {
            get
            {
                return ".vbs";
            }
        }
    }
}