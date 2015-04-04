using System;
using System.Collections.Generic;
using Furesoft.Web.UI.Base;

namespace Furesoft.Web.UI
{
    public class Button : UiElement
    {
        public string Text { get; set; }

        public static implicit operator string(Button btn)
        {
            return btn.ToString();
        }

        public override string ToString()
        {
            var d = new Dictionary<string, string>();
            d.Add("type", "submit");
            d.Add("text", Text);

            return HtmlBuilder.Build(this, false, "input", d);
        }
    }
}