using System;
using Furesoft.Web;
using Language.Lua;
using Mono.Net;
using System.IO;

namespace Lua
{
    public class Language : IScriptLanguage
    {
        public override void Execute(string src, Uri uri, HttpListenerContext p, WebConfig wc, StreamWriter sw)
        {
            LuaTable global = LuaInterpreter.CreateGlobalEnviroment();

            LuaInterpreter.Interpreter(src, global);
        }

        public override string Name
        {
            get
            {
                return "Lua";
            }
        }

        public override string Extension
        {
            get
            {
                return ".lua";
            }
        }

    }
}