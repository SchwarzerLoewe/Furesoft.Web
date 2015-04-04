using System;
using Furesoft.Web.UI.Base;

namespace Furesoft.Web.UI
{
    public class Img : UiElement
    {
        public string Src { get; set; }

        public static implicit operator string(Img btn)
        {
            return btn.ToString();
        }

        public override string ToString()
        {
            return string.Format("<img name='{0}' style='{1}' src='{2}' />", Name, Style, Src);
        }
    }
}