using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Furesoft.Web.Internal;
using Furesoft.Web.Properties;

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

        string folderItem = "<tr><td><a href='{{Path}}/{{folder}}'>{{folder}}</a></td><td align='right'>{{Size}}  </td></tr>";
        string fileItem = "<tr><td><a href='{{Path}}/{{File}}'>{{File}}</a></td><td align='right'>{{Size}}  </td></tr>";
        string parentItem = "<tr><td><a href='{{ParentFolder}}'>Parent Directory</a></td><td>&nbsp;</td><td align='right'>  - </td><td>&nbsp;</td></tr>";

        public void Render(StreamWriter sw)
        {
            if(!isListing()) {
                //ToDo: send access forbidden error
                sw.WriteLine("Access forbidden");

                return;
            }

            string content = Resources.Listing;

            var dict = new Dictionary<string, object>();
            dict.Add("Folder", _p.Request.Url.LocalPath);
            dict.Add("Path", _p.Request.Url.AbsoluteUri);

            content = Template.Render(content, dict);
            folderItem = Template.Render(folderItem, dict);
            fileItem = Template.Render(fileItem, dict);

            var sb = new StringBuilder();

            var pdi = new DirectoryInfo(_fi.ToString());
            parentItem = parentItem.Replace("{{ParentFolder}}", "..");

            sb.AppendLine(parentItem);

            foreach (var folder in Directory.GetDirectories(_fi.ToString()))
            {
                var di = new DirectoryInfo(folder);

                if(folder != "Listing")
                    sb.AppendLine(folderItem.Replace("{{folder}}", di.Name).Replace("{{Size}}", "0"));
            }
            foreach (var file in Directory.GetFiles(_fi.ToString()))
            {
                var fi = new FileInfo(file);

                if (isIgnored(fi.Name))
                    continue;

                sb.AppendLine(fileItem.Replace("{{File}}", fi.Name).Replace("{{Size}}", SizeFormatter.Format(fi.Length, 2)));
            }

            content = content.Replace("{{Items}}", sb.ToString());

            sw.WriteLine(content);
        }

        private bool isIgnored(string name)
        {
            foreach (var item in _ac.IndexIgnore)
            {
                string n = item;
                if(item.StartsWith("*"))
                {
                    n = n.Replace(".", "\\.").Replace("*", ".");
                }

                if (Regex.IsMatch(name, n))
                    return true;
            }

            return false;
        }
        private bool isListing()
        {
            foreach (var o in _ac.Options)
            {
                if (o.Key == "All")
                {
                    if (o.Value == "-Indexes")
                    {
                        return false;
                    }
                    else if (o.Value == "+Indexes")
                    {
                        return true;
                    }
                }
            }

            return true;
        }
    }
}