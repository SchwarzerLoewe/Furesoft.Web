using System;
using System.IO;
using System.Linq;
using Furesoft.Web;
using Mono.Net;

namespace CPP
{
    public class Language : IScriptLanguage
    {
        public override void Execute(string src, HttpListenerContext p, WebConfig wc)
        {
            var bcp = new Microsoft.VisualC.VSCodeProvider();
            var ass = AssemblyInitializer.Init(bcp, src, new StreamWriter(p.Response.OutputStream));
            var page = AssemblyPageResolver.Resolve(ass);

            page.Response = new StreamWriter(p.Response.OutputStream);
            page.Info = new HttpInfo(Get.Create(p), p.Request.Url, wc.DataDir);
            page.IncludePage = new Action<string>(f =>
            {
                new Language().Execute(File.ReadAllText(wc.DataDir + f), p, wc);
            });

            page.OnLoad();
        }

        public override string Name
        {
            get
            {
                return "CPP";
            }
        }

        public override string Extension
        {
            get
            {
                return ".c";
            }
        }

    }
}