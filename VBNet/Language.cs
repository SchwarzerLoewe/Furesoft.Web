using Furesoft.Web;
using Microsoft.VisualBasic;
using Mono.Net;
using System;
using System.CodeDom.Compiler;
using System.IO;

namespace VBNet
{
    public class Language : IScriptLanguage
    {
        public override void Execute(string src, HttpListenerContext p, WebConfig wc, StreamWriter sw)
        {
            var bcp = new VBCodeProvider();
            var options = new CompilerParameters();

            var ass = AssemblyInitializer.Init(bcp, src, sw);
            var page = AssemblyPageResolver.Resolve(ass);

            page.Response = sw;
            page.Info = new HttpInfo(Get.Create(p), p.Request.Url, wc.DataDir, p);
            page.IncludePage = new Action<string>(f =>
            {
                new Language().Execute(File.ReadAllText(wc.DataDir + f), p, wc, sw);
            });
            page.Redirect = new Action<string>(f =>
            {
                p.Response.Redirect(f);
            });

            page.OnLoad();
        }

        public override string Name
        {
            get
            {
                return "VBNet";
            }
        }

        public override string Extension
        {
            get
            {
                return ".vb";
            }
        }
    }
}