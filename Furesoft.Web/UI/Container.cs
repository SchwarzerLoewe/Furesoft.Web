using System;

namespace Furesoft.Web.UI
{
    public class Container
    {
        public string Name { get; set; }
        public string Inner { get; set; }
        public Style Style { get; set; }

        public Container()
        {
            Style = new Style();
        }

        public static implicit operator string(Container btn)
        {
            return btn.ToString();
        }

        public override string ToString()
        {
            return string.Format("<div name='{0}' style='{1}'>{2}</div>", Name, Style, Inner);
        }
    }
}