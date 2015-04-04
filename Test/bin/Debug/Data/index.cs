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
		btn.Inner = "Click Me";
		btn.Href = "../include/test";
		btn.Style.Append("text-decoration", "none");
		btn.Style.Append("color", "#DC143C");

        var b = new Button();
        b.Name = "hello";
        b.Text = "Click Me";

        var buff = new Uri("http://dotnet-snippets.de/images/user/nopic.png").FromWeb();

        var canvas = new DynamicImage(100, 100);
        canvas.FillRectangle(new Rectangle(10, 10, 10, 10), Color.Green);

        var img = new Img();
        img.Src = Converter.ToWebString(buff, ImageFormat.Png);
        img.Style.Append("margin", "10");

        btn.Inner = img;

        var div = new Container();
        div.Name = "divcontainer";

        div.Style.Append("background-image", "url(" + canvas + ")");
        div.Style.Append("height", "250");
        div.Style.Append("width", "450");

        div.Inner += btn;
        div.Inner += b;

        Response.WriteLine(div);
		
        if (Isset(Get["include"]))
        {
            IncludePage("test.cs");
        }
	}
	
}