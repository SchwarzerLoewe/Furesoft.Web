using Furesoft.Web.Internal;
using Mono.Net;
using System;
using System.Collections.Generic;
using System.IO;

namespace Furesoft.Web
{
    public class StandardScriptApi
    {
        public Dictionary<string, Delegate> Functions = new Dictionary<string, Delegate>();
        public Dictionary<string, object> Variables = new Dictionary<string, object>();
        public Dictionary<string, Type> Types = new Dictionary<string, Type>();

        public StandardScriptApi(Uri uri, HttpListenerContext p, StreamWriter sw)
        {
            Functions.Add("echo", new Action<object>(sw.Write));
            Functions.Add("include", new Action<string>(pa => sw.Write(File.ReadAllText(pa))));
            Functions.Add("isset", new Func<object, bool>(o =>
            {
                return isset(o);
            }));
            Functions.Add("CreateObj", new Func<string, object>(o =>
            {
                return Activator.CreateInstance(Type.GetTypeFromProgID(o));
            }));
            Functions.Add("import", new Func<string, object>(o =>
            {
                return Activator.CreateInstance(Type.GetType(o));
            }));

            Variables.Add("OutputStream", sw);
            Variables.Add("_GET", Get.Create(uri));

            //Types.Add("XmlHttpRequest", typeof(XmlHttpRequest));
            Types.Add("Map", typeof(Map));
        }
  
        public static bool isset(object o)
        {
            return !(o == null || o == string.Empty);
        }
    }
}