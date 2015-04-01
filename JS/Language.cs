using Furesoft.Web;
using Mono.Net;
using System;
using System.IO;

namespace JS
{
    public class Language : IScriptLanguage
    {
        public override string Name
        {
            get
            {
                return "JS";
            }
        }

        public override string Extension
        {
            get
            {
                return ".jsa";
            }
        }

        public override void Execute(string src, HttpListenerContext p, WebConfig wc, StreamWriter sw)
        {
            JS.JScriptEngine engine = new JScriptEngine();
            var sapi = new StandardScriptApi(p, sw);

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

            engine.Execute(src);
        }
    }
}