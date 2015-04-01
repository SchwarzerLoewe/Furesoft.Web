using System;

namespace Furesoft.Web.Service.Internal
{
    public class Get
    {
        public static Map Create(HttpProcessor p)
        {
            var props = new Map();

            var uri = new Uri("htt://localhost" + p.http_url);
            var q = uri.Query;

            foreach (var qi in q.Split(new[] { '&', '?' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var s = qi.Split('=');

                props.Add(s[0], s[1]);
            }

            return props;
        }
    }
}