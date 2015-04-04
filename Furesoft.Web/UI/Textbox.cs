using Furesoft.Web.UI.Base;
using System;

namespace Furesoft.Web.UI
{
    public class Textbox : UiElement
    {
        public static implicit operator string(Textbox btn)
        {
            return btn.ToString();
        }

        public override string ToString()
        {
            return string.Format("<input type='text' name='{0}' style='{1}' />", Name, Style);
        }
    }
}