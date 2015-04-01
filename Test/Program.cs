using Furesoft.Web;
using System;
using System.Windows.Forms;

namespace Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //wc.ScriptingLanguages.Add(new JS.Language());
            //wc.ScriptingLanguages.Add(new VBScript.Language());

            // wc.ScriptingLanguages.Add(new PHP.Language());

            //wc.ScriptingLanguages.Add(new FSharp.Language());

            //wc.ScriptingLanguages.Add(new VBNet.Language());
            //wc.ScriptingLanguages.Add(new JScriptNet.Language());

            //wc.ScriptingLanguages.Add(new VBScript.Language());
            //wc.ScriptingLanguages.Add(new CPP.Language());
            //wc.ScriptingLanguages.Add(new Python.Language());
            //wc.ScriptingLanguages.Add(new Boo.Language());
            //wc.ScriptingLanguages.Add(new Lua.Language());
            //wc.ScriptingLanguages.Add(new EcLang.Language());
            //wc.ScriptingLanguages.Add(new Ruby.Language());
            //wc.ScriptingLanguages.Add(new Scheme.Language());

            var ws = WebServer.Open("server://127.0.0.1:8080/");
            ws.WebConfig.DataDir = Application.StartupPath + "\\Data\\";
            ws.WebConfig.ScriptingLanguages.Add(new CSharp.Language());
            ws.Start();

            Console.WriteLine("A test webserver.");

            Console.ReadKey();
            // ws.Stop();
        }
    }
}