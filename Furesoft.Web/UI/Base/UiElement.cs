using System;

namespace Furesoft.Web.UI.Base
{
    public abstract class UiElement
    {
        public string Name { get; set; }
        public Style Style { get; set; }
        public string Class { get; set; }
        public string Id { get; set; }
        public string Inner { get; set; }

        public UiElement()
        {
            Style = new Style();
        }
    }
}