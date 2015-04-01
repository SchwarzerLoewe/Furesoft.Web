using System;
using System.IO;
using System.Reflection;
using Furesoft.Web;
using Furesoft.Web.Internal;

namespace Dll
{
    public class Language : IScriptLanguage
    {
        public override string Name
        {
            get
            {
                return "Dynamic Link Library";
            }
        }

        public override string Extension
        {
            get
            {
                return ".dll";
            }
        }

        public override void Execute(string src, HttpProcessor p)
        {
            File.WriteAllText("dll.dll", src);
            var ass = Assembly.Load("dll.dll");

        }
    }
}