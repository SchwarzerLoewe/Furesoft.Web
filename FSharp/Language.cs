﻿using FSharp.Compiler.CodeDom;
using Furesoft.Web;
using Mono.Net;
using System;
using System.IO;

namespace FSharp
{
    public class Language : IScriptLanguage
    {
        public override void Execute(string src, HttpListenerContext p, WebConfig wc, StreamWriter sw)
        {
            var bcp = new FSharpCodeProvider();
            var ass = AssemblyInitializer.Init(bcp, src, new StreamWriter(p.Response.OutputStream));
            var page = AssemblyPageResolver.Resolve(ass);

            page.Response = new StreamWriter(p.Response.OutputStream);
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
                return "FSharp";
            }
        }

        public override string Extension
        {
            get
            {
                return ".fs";
            }
        }
    }
}