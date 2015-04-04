using System;
using System.Collections.Generic;
using System.Text;

namespace Furesoft.Web.UI.Base
{
    public static class HtmlBuilder
    {
        public static string Build(object obj, bool generateInner, string name = null, Dictionary<string, string> defaultAtts = null)
        {
            var props = obj.GetType().GetProperties();
            Dictionary<string, string> atts = new Dictionary<string, string>();

            if (obj.GetType().BaseType.Name == typeof(UiElement).Name)
            {
                foreach (var item in props)
                {
                    if (item.PropertyType.Name == typeof(string).Name)
                    {
                        atts.Add(item.Name, (string)item.GetValue(obj, null));
                    }
                    if (item.PropertyType.Name == typeof(Style).Name)
                    {
                        atts.Add(item.Name, ((Style)item.GetValue(obj, null)).ToString());
                    }
                }
                if (defaultAtts != null)
                {
                    foreach (var item in defaultAtts)
                    {
                        atts.Add(item.Key, item.Value);
                    }
                }

                var sb = new StringBuilder();

                if (name != null)
                {
                    sb.Append("<" + name + " ");
                }
                else
                {
                    sb.Append("<" + obj.GetType().Name.ToLower() + " ");
                }

                foreach (var a in atts)
                {
                    if (a.Key != "Inner" && !string.IsNullOrEmpty(a.Value))
                    {
                        sb.Append(a.Key.ToLower() + "='" + a.Value + "' ");
                    }
                }

                if (!generateInner)
                {
                    sb.Append("/>");
                }
                else
                {
                    sb.Append(">");

                    if (name != null)
                    {
                        sb.Append(atts["Inner"] + "</" + name + ">");
                    }
                    else
                    {
                        sb.Append(atts["Inner"] + "</" + obj.GetType().Name.ToLower() + ">");
                    }
                }

                return sb.ToString();
            }

            return string.Empty;
        }
    }
}