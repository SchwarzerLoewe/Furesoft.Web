using Furesoft.Web.Internal;
using System;
using System.Text.RegularExpressions;

namespace Furesoft.Web.Modules
{
    public class RewriteRule
    {
        public static void Match(Uri uri, Access ac, out string fi)
        {
            string v = "";
            try
            {
                foreach (var rr in ac.RewriteRule)
                {
                    var r = new Regex("(" + rr.Key + ")");
                    Match m = r.Match(uri.PathAndQuery);
                    if (m.Success)
                    {
                        string url = rr.Value;
                        v = Capturize(m.Groups, url);
                    }
                }
            }
            catch { }

            fi = v;
        }

        private static string Capturize(GroupCollection cc, string p)
        {
            string tmp = p;
            for (int i = 2; i < cc.Count+2; i++)
            {
                var inn = (i + 1);
                tmp = tmp.Replace("$" + (i-1), cc[i].Value);
            }

            return tmp;
        }
    }
}