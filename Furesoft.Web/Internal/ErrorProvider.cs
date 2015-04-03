using System;
using System.IO;
using Furesoft.Web.Modules;

namespace Furesoft.Web.Internal
{
    public static class ErrorProvider
    {
        private static Access _ac;
        private static WebConfig _wc;

        public static void Init(Access ac, WebConfig wc) {
            _ac = ac;
            _wc = wc;
        }

        public static string GetHtml(int errorcode)
        {
            if (_ac.ErrorDocument.ContainsKey(errorcode.ToString()))
            {
                var tmp = _ac.ErrorDocument[errorcode.ToString()];
                var fi = new FileInfo(tmp.Replace("{data}", _wc.DataDir) + "\\");

                if (fi.Exists)
                {
                    var er = File.ReadAllText(tmp);

                    LoggerModule.Log(er);

                    return er;
                }

                return tmp;
            }
            else
            {
                return "<h1>Error " + errorcode + "</h1>";
            }
        }
    }
}