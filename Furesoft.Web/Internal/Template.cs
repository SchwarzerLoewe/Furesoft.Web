using System;
using System.Collections.Generic;
using System.Linq;

namespace Furesoft.Web.Internal
{
    public class Template
    {
        public static string Render(string src, Dictionary<string, object> values)
        {
            foreach (var val in values)
            {
                src = src.Replace("{{" + val.Key + "}}", val.Value.ToString());
            }

            return src;
        }

    }
}