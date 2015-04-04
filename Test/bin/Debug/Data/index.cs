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
        btn.Visible = false;

        var b = new Button();
        b.Name = "hello";
        b.Text = "Click Me";

        var buff = new Uri("http://dotnet-snippets.de/images/user/nopic.png").FromWeb();

        var canvas = new DynamicImage(100, 100);
        canvas.FillRectangle(new Rectangle(20, 20, 50, 50), Color.Green);

        string ci = "<image width='100' height='100'><frec color='Green' x='0' y='0' width='90' heigth='90' /><frec color='Blue' x='20' y='20' width='50' heigth='50' /></image>";
        canvas = DynamicImage.FromXml(ci);

        var img = new Img();
        img.Src = Converter.ToWebString(buff, ImageFormat.Png);
        img.Style.Append("margin-right", "25%");
        img.Style.Append("margin-left", "25%");

        btn.Inner = img;

        var div = new Container();
        div.Name = "divcontainer";

        div.Style.Append("background-image", "url(" + canvas + ")");
        div.Style.Append("height", "100%");
        div.Style.Append("width", "100%");

        div.Inner += btn;
        div.Inner += b;

        Response.WriteLine(div);
		
        if (Isset(Get["include"]))
        {
            IncludePage("test.cs");
        }
	}
	
}