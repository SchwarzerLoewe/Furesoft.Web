using System;
using System.Xml.Linq;

namespace Furesoft.Web.UI.Base
{
    public abstract class UiElement
    {
        public string Name { get; set; }
        public Style Style { get; set; }
        public string Class { get; set; }
        public string Id { get; set; }
        public string Inner { get; set; }
        public bool Visible { get; set; }

        public UiElement()
        {
            Style = new Style();
            Visible = true;
        }

        public static string Tidy(string xml)
        {
            try
            {
                XDocument doc = XDocument.Parse(xml);
                return doc.ToString();
            }
            catch (Exception)
            {
                return xml;
            }
        }
    }
}