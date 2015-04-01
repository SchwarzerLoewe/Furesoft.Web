using System;
using System.Collections.Generic;
using System.Text;

namespace Furesoft.Web.UI
{
    public class Style
    {
        private Dictionary<string, string> data = new Dictionary<string, string>();

        public void Append(string name, string value)
        {
            data.Add(name, value);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var kv in data)
            {
                sb.Append(kv.Key + ": " + kv.Value + ";");
            }

            return sb.ToString();
        }
    }
}