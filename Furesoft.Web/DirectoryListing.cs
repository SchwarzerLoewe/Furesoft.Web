using System;
using System.IO;
using System.Windows.Forms;

namespace Furesoft.Web
{
    public class DirectoryListing
    {
        private Internal.Access _ac;
        private Mono.Net.HttpListenerContext _p;
        private FileInfo _fi;

        public DirectoryListing(Internal.Access ac, Mono.Net.HttpListenerContext p, FileInfo fi)
        {
            this._ac = ac;
            this._p = p;
            _fi = fi;
        }

        public void Render(StreamWriter sw)
        {
            string content = File.ReadAllText(Application.StartupPath + "\\Listing\\index.html.htm");

            content = content.Replace("{{Folder}}", _p.Request.Url.LocalPath).Replace("{{ListingPath}}", "file://" + Application.StartupPath + "\\Listing\\");

            sw.WriteLine(content);
        }
    }
}