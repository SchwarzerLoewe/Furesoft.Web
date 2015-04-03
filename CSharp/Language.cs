using Furesoft.Web;
using Furesoft.Web.Internal;
using Microsoft.CSharp;
using Mono.Net;
using System;
using System.CodeDom.Compiler;
using System.IO;

namespace CSharp
{
    public class Language : IScriptLanguage
    {
        IniFile ini;

        public override void Execute(string src, Uri uri, HttpListenerContext p, WebConfig wc, StreamWriter sw)
        {
            var bcp = new CSharpCodeProvider();
            var ass = AssemblyInitializer.Init(bcp, src, sw, ini);

            var page = AssemblyPageResolver.Resolve(ass);

            page.Response = sw;

            uri = new Uri("http://localhost/" + Path.GetFileName(uri.LocalPath));

            page.Info = new HttpInfo(Get.Create(uri), uri, wc.DataDir, p);
            page.IncludePage = new Action<string>(f =>
            {
                new Language().Execute(File.ReadAllText(wc.DataDir + f), uri, p, wc, sw);
            });
            page.Redirect = new Action<string>(f =>
            {
                p.Response.Redirect(f);
            });
            page.Isset = new Predicate<object>(b => {
                return StandardScriptApi.isset(b);
            });

            page.Get = page.Info.Get;

            page.OnLoad();
        }

        public override void Load()
        {
            ini = new IniFile("csharp.ini");
            
        }

        public override dynamic Evaluate(string src, string classname, HttpListenerContext p, WebConfig wc)
        {
            var bcp = new CSharpCodeProvider();
            var options = new CompilerParameters();

            var ass = AssemblyInitializer.Init(bcp, src, new StreamWriter(p.Response.OutputStream), ini);

            foreach (var a in ass.GetTypes())
            {
                if (a.BaseType.Name == classname)
                {
                    return ass.CreateInstance(a.FullName);
                }
            }

            return null;
        }

        public override string Name
        {
            get
            {
                return "CSharp";
            }
        }

        public override string Extension
        {
            get
            {
                return ".cs";
            }
        }
    }
}