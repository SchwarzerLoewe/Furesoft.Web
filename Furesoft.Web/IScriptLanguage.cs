using System;
using Mono.Net;
using System.IO;

namespace Furesoft.Web
{
    public abstract class IScriptLanguage
    {
        public abstract string Name { get; }

        public abstract string Extension { get; }

        public abstract void Execute(string src, Uri uri, HttpListenerContext p, WebConfig wc, StreamWriter sw);

        public virtual void Load()
        {
            return;
        }

        public virtual dynamic Evaluate(string src, string classname, HttpListenerContext p, WebConfig wc)
        {
            return null;
        }
    }
}