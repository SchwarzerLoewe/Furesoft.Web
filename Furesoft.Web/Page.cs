using System;
using System.IO;
using Furesoft.Web.Internal;

namespace Furesoft.Web
{
    public abstract class Page
    {
        public StreamWriter Response;
        public HttpInfo Info;
        public Action<string> IncludePage;
        public Action<string> Redirect;
        public Predicate<object> Isset;
        public Map Get;

        public Page NewPage()
        {
            return new DynamicPage();
        }

        public abstract void OnLoad();
    }
}