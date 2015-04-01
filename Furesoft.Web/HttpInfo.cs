using Furesoft.Web.Internal;
using Mono.Net;
using System;

namespace Furesoft.Web
{
    public class HttpInfo
    {
        public Map Get;
        public Uri Uri;
        public string Data;
        public HttpListenerContext Context;

        public HttpInfo(Map get, Uri uri, string data, HttpListenerContext c)
        {
            Get = get;
            Uri = uri;
            Data = data;
            Context = c;
        }
    }
}