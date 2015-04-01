using Furesoft.Web;
using Furesoft.Web.UI;

public class Home : Page {
	
	public override void OnLoad() {
		var link = new Link();
		
		link.Text = "Zur&uuml;ck";
		link.Name = "name";
		link.Href = "http://127.0.0.1:8080/include/";

		Response.WriteLine(link);
	}
	
}