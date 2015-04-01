using Furesoft.Web;
using Mono.Net;
using System.IO;

namespace Ruby
{
    public class Language : IScriptLanguage
    {
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

        public override void Execute(string src, HttpListenerContext p, WebConfig wc, StreamWriter sw)
        {
            Microsoft.Scripting.Hosting.ScriptEngine py = IronRuby.Ruby.CreateEngine();
            Microsoft.Scripting.Hosting.ScriptScope s = py.CreateScope();

            var sapi = new StandardScriptApi(p, sw);
            foreach (var item in sapi.Variables)
            {
                s.SetVariable(item.Key, item.Value);
            }

            py.Execute(src, s);
        }
    }
}