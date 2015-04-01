using Furesoft.Web;
using Furesoft.Web.UI;
using System.Net;

public class Home : Page {
	
	public override void OnLoad() {
		var btn = new Link();
		btn.Name = "lnk";
		btn.Text = "Click Me";
		btn.Href = "../include/test";
		btn.Style.Append("text-decoration", "none");
		btn.Style.Append("color", "#DC143C");
		
		Response.Write(btn);
		
        if (Isset(Get["include"]))
        {
            IncludePage("test.cs");
        }
	}
	
}