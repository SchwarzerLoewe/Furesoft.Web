using System;

namespace Furesoft.Web.UI
{
    public class Img
    {
        public string Name { get; set; }
        public string Src { get; set; }
        public Style Style { get; set; }

        public Img()
        {
            Style = new Style();
        }

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