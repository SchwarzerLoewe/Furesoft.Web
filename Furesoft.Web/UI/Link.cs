using System;
using Furesoft.Web.UI.Base;

namespace Furesoft.Web.UI
{
    public class Link : UiElement
    {
        public string Href { get; set; }

        public static implicit operator string(Link btn)
        {
            return btn.ToString();
        }

        public override string ToString()
        {
            return string.Format("<a name='{0}' href='{1}' style='{2}'>{3}</a>", Name, Href, Style, Inner);
        }
    }
}