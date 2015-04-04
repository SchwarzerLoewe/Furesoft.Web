using Furesoft.Web;
using Furesoft.Web.UI;
using System.Net;
using System.Drawing;
using System.Drawing.Imaging;
using System;

public class Home : Page {
	
	public override void OnLoad() {
        var buff = new Uri("http://dotnet-snippets.de/images/user/nopic.png").FromWeb();

        var body = new Body();

        body.Style.Append("backgroundcolor", "#C0FFEE");

        string ci = "<image width='100' height='100'><frec color='Green' x='0' y='0' width='90' heigth='90' /><frec color='Blue' x='20' y='20' width='50' heigth='50' /></image>";
        var canvas = DynamicImage.FromXml(ci);

        var img = new Img();
        img.Src = Converter.ToWebString(buff, ImageFormat.Png);
        img.Style.Append("margin-right", "25%");
        img.Style.Append("margin-left", "25%");

        canvas.DrawImage(new Point(0, 0), img.Src);

        var div = new Container();
        div.Name = "divcontainer";

        div.Style.Append("background-image", "url(" + canvas + ")");
        div.Style.Append("background-repeat", "no-repeat");
        div.Style.Append("height", "100%");
        div.Style.Append("width", "100%");

        div.Inner = img;

        body.Inner = div;

        Response.WriteLine(body);
		
        if (Isset(Get["include"]))
        {
            IncludePage("test.cs");
        }
	}
	
}