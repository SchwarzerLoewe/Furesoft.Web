using System;

namespace Furesoft.Web.UI
{
    public class Button
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public Style Style { get; set; }

        public Button()
        {
            Style = new Style();
        }

        public override string ToString()
        {
            return string.Format("<button typ='submit' name='{0}' style='{1}'>{2}</input>", Name, Style, Text);
        }
    }
}