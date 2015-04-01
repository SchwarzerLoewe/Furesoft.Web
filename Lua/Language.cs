using Furesoft.Web;
using Language.Lua;
using Mono.Net;
using System.IO;

namespace Lua
{
    public class Language : IScriptLanguage
    {
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

        public override void Execute(string src, HttpListenerContext p, WebConfig wc, StreamWriter sw)
        {
            LuaTable global = LuaInterpreter.CreateGlobalEnviroment();

            LuaInterpreter.Interpreter(src, global);
        }
    }
}