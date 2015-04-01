using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using Furesoft.Web.Internal;
using Mono.Net;
using System;

namespace Furesoft.Web
{
    public class Get
    {
        public static Map Create(Uri uri)
        {
            var props = new Map();

            if (uri.Query != "")
            {
                NameValueCollection queryParameters = new NameValueCollection();
                string[] querySegments = uri.Query.Split('&');
                foreach (string segment in querySegments)
                {
                    string[] parts = segment.Split('=');
                    if (parts.Length > 0)
                    {
                        string key = parts[0].Trim(new char[] { '?', ' ' });

                        string val = "True";

                        if (parts.Length == 2)
                        {
                            val = parts[1].Trim();
                        }

                        props.Add(key, val);
                    }
                }
            }

            return props;
        }
    }
}