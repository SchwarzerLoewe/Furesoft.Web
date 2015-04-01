using System.Collections.Specialized;
using System.IO;
using System.Net;

namespace Furesoft.Web.Internal
{
    public class PostRequest
    {
        public static Stream GetResponse(string uri, NameValueCollection data)
        {
            var wc = new WebClient();
            return new MemoryStream(wc.UploadValues(uri, data));
        }
    }
}