using System;
using Furesoft.Web.UI.Base;

namespace Furesoft.Web.UI
{
    public class Body : UiElement
    {
        public string Text { get; set; }

        public static implicit operator string(Body btn)
        {
            return btn.ToString();
        }

        public override string ToString()
        {
            return HtmlBuilder.Build(this, true, "body", null);
        }
    }
}