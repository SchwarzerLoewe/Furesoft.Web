using System.Collections.Generic;

namespace Furesoft.Web
{
    public class WebConfig
    {
        public string DataDir;
        public Dictionary<string, string> ErrorPages = new Dictionary<string, string>();
        public List<IScriptLanguage> ScriptingLanguages = new List<IScriptLanguage>();
        public int Port;
        public string IPAddress;
        public string ConfigPath = "\\Config\\";
    }
}