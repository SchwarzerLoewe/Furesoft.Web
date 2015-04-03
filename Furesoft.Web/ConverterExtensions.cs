using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace Furesoft.Web
{
    public static class Converter
    {
        public static string ToString(this byte[] raw)
        {
            return Convert.ToBase64String(raw);
        }

        public static byte[] ToBytes(this string src)
        {
            return Convert.FromBase64String(src);
        }

        public static string ToWebString(this Image img, ImageFormat iformat)
        {
            var ms = new MemoryStream();
            img.Save(ms, iformat);

            return "data:image/" + new ImageFormatConverter().ConvertToString(iformat).ToLower() + ";base64," + ToString(ms.ToArray());
        }

        public static Image FromWebString(this string src)
        {
            var regex = new Regex(@"data:(?<mime>[\w/\-\.]+);(?<encoding>\w+),(?<data>.*)", RegexOptions.Compiled);

            var match = regex.Match(src);

            var mime = match.Groups["mime"].Value;
            var encoding = match.Groups["encoding"].Value;
            var data = match.Groups["data"].Value;

            if (encoding == "base64")
            {
                var ms = new MemoryStream(data.ToBytes());

                return Image.FromStream(ms);
            }

            return null;
        }

        public static Image FromWeb(this Uri src)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(src);
            HttpWebResponse httpWebReponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream stream = httpWebReponse.GetResponseStream();

            return Image.FromStream(stream);
        }

        public static byte[] ToArray(this Stream strm)
        {
            var ms = new MemoryStream();

            strm.CopyTo(ms);

            return ms.ToArray();
        }
    }
}