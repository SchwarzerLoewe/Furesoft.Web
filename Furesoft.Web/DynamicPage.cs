using System;

namespace Furesoft.Web
{
    class DynamicPage : Page
    {
        public DynamicPage()
        {

        }

        public void Render(string html)
        {
            this.Response.WriteLine(html);
        }

        public override void OnLoad()
        {
            return;
        }
    }
}