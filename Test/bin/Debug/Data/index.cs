using Furesoft.Web;
using Furesoft.Web.UI;
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;
using System;

public class Home : Page {
	
	public override void OnLoad() {
		var btn = new Link();
		btn.Name = "lnk";
		btn.Text = "Click Me";
		btn.Href = "../include/test";
		btn.Style.Append("text-decoration", "none");
		btn.Style.Append("color", "#DC143C");

        var buff = new Uri("http://dotnet-snippets.de/images/user/nopic.png").FromWeb();

        var img = new Img();
        img.Src = Converter.ToWebString(buff, ImageFormat.Png);

		Response.Write(btn);
        Response.WriteLine(img);
		
        if (Isset(Get["include"]))
        {
            IncludePage("test.cs");
        }
	}
	
}