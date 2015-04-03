using System;

namespace Furesoft.Web.UI
{
    public class Button
    {
        public string Name { get; set; }
        public string Inner { get; set; }
        public Style Style { get; set; }

        public Button()
        {
            Style = new Style();
        }

        public static implicit operator string(Button btn)
        {
            return btn.ToString();
        }

        public override string ToString()
        {
            return string.Format("<button typ='submit' name='{0}' style='{1}'>{2}</input>", Name, Style, Inner);
        }
    }
}