using System;
using Furesoft.Web.UI.Base;

namespace Furesoft.Web.UI
{
    public class Container : UiElement
    {
        public static implicit operator string(Container btn)
        {
            return btn.ToString();
        }

        public override string ToString()
        {
            return Tidy(HtmlBuilder.Build(this, true, "div", null));
        }
    }
}