using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Furesoft.Web.Internal;

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

        string folderItem = "<tr><td valign='top'><img src='{{ListingPath}}/folder.gif' alt='[DIR]'></td><td><a href='{{Path}}/{{folder}}'>{{folder}}</a></td><td align='right'>{{Size}}  </td></tr>";
        string fileItem = "<tr><td valign='top'><img src='{{ListingPath}}/unknown.gif' alt='[FILE]'></td><td><a href='{{Path}}/{{File}}'>{{File}}</a></td><td align='right'>{{Size}}  </td></tr>";
        public void Render(StreamWriter sw)
        {
            string content = File.ReadAllText(Application.StartupPath + "\\Data\\Listing\\index.htm");

            var dict = new Dictionary<string, object>();
            dict.Add("Folder", _p.Request.Url.LocalPath);
            dict.Add("ListingPath", "http://" + Application.StartupPath + "\\Listing\\");
            dict.Add("Path", _p.Request.Url.AbsoluteUri);

            content = Template.Render(content, dict);
            folderItem = Template.Render(folderItem, dict);

            var sb = new StringBuilder();

            foreach (var folder in Directory.GetDirectories(_fi.ToString()))
            {
                var di = new DirectoryInfo(folder);

                if(folder != "Listing")
                    sb.AppendLine(folderItem.Replace("{{folder}}", di.Name).Replace("{{Size}}", "0"));
            }
            foreach (var file in Directory.GetFiles(_fi.ToString()))
            {
                var fi = new FileInfo(file);

                sb.AppendLine(fileItem.Replace("{{File}}", fi.Name).Replace("{{Size}}", fi.Length.ToString()));
            }

            content = content.Replace("{{Items}}", sb.ToString());

            sw.WriteLine(content);
        }
    }
}