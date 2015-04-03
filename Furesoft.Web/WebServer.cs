using System.Web;
using Furesoft.Web.Internal;
using Furesoft.Web.Internal.HtAccess;
using Furesoft.Web.Modules;
using Mono.Net;
using System;
using System.IO;
using System.Text;
using System.Threading;

namespace Furesoft.Web
{
    public class WebServer
    {
        private HttpListener ws;
        private WebConfig _wc;
        private Access ac;

        public WebConfig WebConfig
        {
            get
            {
                return _wc;
            }
        }

        public WebServer(WebConfig wc)
        {
            ws = new HttpListener();
            ac = new Access("");
            _wc = wc;

            ws.Prefixes.Add("http://" + wc.IPAddress + ":" + wc.Port + "/");
            ws.Prefixes.Add("http://localhost:" + wc.Port + "/");

            LoggerModule.Init();
            ErrorProvider.Init(ac, _wc);
        }

        public static WebServer Open(string connectionstring)
        {
            var uri = new Uri(connectionstring);

            return new WebServer(new WebConfig() { Port = uri.Port, IPAddress = uri.Host.ToString() });
        }

        private bool ContainsLang(FileInfo uri)
        {
            foreach (var sl in _wc.ScriptingLanguages)
            {
                if (sl.Extension == uri.Extension)
                {
                    return true;
                }
            }
            return false;
        }

        private IScriptLanguage GetLanguageByExtension(FileInfo uri)
        {
            foreach (var sl in _wc.ScriptingLanguages)
            {
                if (sl.Extension == uri.Extension)
                {
                    return sl;
                }
            }
            return null;
        }

        private void HandleFile(FileInfo f, Uri uri, HttpListenerContext p, StreamWriter sw)
        {
            using (var reader = new System.IO.StreamReader(f.ToString(), true))
            {
                var fs = reader.BaseStream.ToArray();

                p.Response.ContentType = Mime.GetMimeType(f.Extension);

                if (!ContainsLang(f))
                {
                    if (Mime.GetMimeType(f.Extension).StartsWith("image"))
                    {
                        p.Response.OutputStream.Write(fs, 0, fs.Length);
                        p.Response.OutputStream.Flush();
                    }
                    else
                    {
                        sw.WriteLine(reader.CurrentEncoding.GetString(fs));
                        sw.Flush();
                    }
                }
                else
                {
                    var lng = GetLanguageByExtension(f);
                    lng.Load();

                    lng.Execute(reader.CurrentEncoding.GetString(fs), uri, p, _wc, sw);

                    LoggerModule.Log(lng.Name + " Script executed");
                }
            }
        }

        private Thread t;

        public void Start()
        {
            ws.Start();

            t = new Thread(loop);
            t.Start();
        }

        private void loop()
        {
            while (ws.IsListening)
            {
                HandleRequest(ws.GetContext());
            }
        }

        public void Stop()
        {
            ws.Stop();
        }

        private bool authed;

        public void HandleRequest(HttpListenerContext p)
        {
            var uri = p.Request.Url;
            LoggerModule.Log("request " + uri.AbsolutePath);

            var sw = new StreamWriter(p.Response.OutputStream);

            var f = new FileInfo(_wc.DataDir + uri.AbsolutePath);

            if (Access.HasAccess(f.DirectoryName))
            {
                var htaccess = new Furesoft.Web.Internal.HtAccess.Parser();

                htaccess.AddContstant("HTTP_HOST", uri.Host);
                htaccess.AddContstant("HTTP_URI", uri.ToString());
                htaccess.AddContstant("HTTP_PATH", uri.LocalPath);

                if (f.Exists)
                {
                    htaccess.Parse(File.ReadAllText(f.DirectoryName + @"\.htaccess"));
                }
                else
                {
                    htaccess.Parse(File.ReadAllText(_wc.DataDir + @"\.htaccess"));
                }

                ac = new Access(f.DirectoryName);

                //htaccess.PopulateCondition("*" + Path.GetExtension(f.Name), ac);
                htaccess.Populate(ac);

                AccessModule.Init(ac);

                if (ac.Redirect.Count > 0)
                {
                    foreach (var r in ac.Redirect)
                    {
                        if (uri.AbsolutePath == "/" + r.Key)
                        {
                            uri = new Uri(_wc.DataDir + r.Key);

                            string uris = HttpUtility.UrlDecode(uri.ToString()).Replace("file:///", "");
                            uri = new Uri(uris);

                            if (uris.Contains("?"))
                            {
                                uris = uris.Substring(0, uris.IndexOf("?"));
                            }

                            f = new FileInfo(uris);

                            p.Response.Redirect(r.Value);
                            return;
                        }
                    }
                }

                if (ac.RewriteEngine)
                {
                    var ff = "";
                    RewriteRule.Match(uri, ac, out ff);

                    if (ff != "")
                    {
                        uri = new Uri(_wc.DataDir + ff);
                        string uris = HttpUtility.UrlDecode(uri.ToString()).Replace("file:///", "");
                        uri = new Uri(uris);

                        if (uris.Contains("?"))
                        {
                            uris = uris.Substring(0, uris.IndexOf("?"));
                        }

                        f = new FileInfo(uris);
                    }
                }

                foreach (var er in ac.ErrorDocument)
                {
                    if (!_wc.ErrorPages.ContainsKey(er.Key))
                    {
                        _wc.ErrorPages.Add(er.Key, er.Value);
                    }
                }

                if (htaccess.GetCommand<Directive>("AuthUserFile") != null)
                {
                    ac.ReadUserFile();
                }
            }

            if (AccessModule.IsBlocked(p.Request.LocalEndPoint.Address.ToString()))
            {
                sw.WriteLine(ErrorProvider.GetHtml(403));
                LoggerModule.Log(ErrorProvider.GetHtml(403));

                return;
            }

            if (ac.AuthType == "Basic")
            {
                if (!authed)
                {
                   // ws.AuthenticationSchemes = AuthenticationSchemes.Basic;
                    ws.AuthenticationSchemeSelectorDelegate = (s) =>
                    {
                        try
                        {
                            if (p.User.Identity.IsAuthenticated)
                            {
                                LoggerModule.Log("Authentication requested");

                                foreach (var u in ac.Users)
                                {
                                    var identity = (HttpListenerBasicIdentity)p.User.Identity;

                                    if (identity.Name == u.Username && identity.Password == u.Password)
                                    {
                                        //    authed = true;
                                        return AuthenticationSchemes.None;
                                    }
                                    else
                                    {
                                        p.Response.StatusCode = 403;
                                        p.Response.OutputStream.Seek(10, SeekOrigin.Current);

                                        sw.WriteLine("<p>Access Denied</p>");

                                        LoggerModule.Log(ErrorProvider.GetHtml(403));
                                    }
                                }
                            }
                        }
                        catch
                        {
                        }
                        if (ac.AuthType == "Basic")
                            return AuthenticationSchemes.Basic;
                        else
                            return AuthenticationSchemes.None;
                    };
                }

                if (File.Exists(f.ToString()))
                {
                    HandleFile(f, uri, p, sw);
                }
                else if (!uri.IsFile && Directory.Exists(f.ToString()))
                {
                    var index = "index.html";

                    if (ac != null)
                    {
                        if (ac.DirectoryIndex != null)
                        {
                            index = ac.DirectoryIndex;
                        }
                    }

                    if (!File.Exists(f.ToString() + index))
                    {
                        new DirectoryListing(ac, p, f).Render(sw);
                    }
                    else
                    {
                        HandleFile(new FileInfo(f.ToString() + index), uri, p, sw);
                    }
                }
                else
                {
                    sw.WriteLine(ErrorProvider.GetHtml(404));
                    LoggerModule.Log(ErrorProvider.GetHtml(404));
                }

                p.Response.OutputStream.Flush();
                sw.Flush();
                p.Response.Close();
                sw.Close();
            }
            /*
            public byte[] SendResponse(HttpListenerRequest request, HttpListenerResponse response)
            {
            var f = new FileInfo(_wc.DataDir + request.Url.AbsolutePath);
            if (request.HttpMethod == "GET")
            {
            if(Access.HasAccess(f.DirectoryName))
            {
            var htaccess = new Furesoft.Web.Internal.HtAccess.Parser();
            htaccess.Parse(File.ReadAllText(f.DirectoryName + @"\.htaccess"));
            var ac = new Access(f.DirectoryName);
            htaccess.PopulateCondition("*" + Path.GetExtension(f.Name), ac);
            htaccess.Populate(ac);
            if (htaccess.GetCommand<Directive>("AuthUserFile") != null)
            {
            ac.AuthUserFile = (string)htaccess.GetCommand<Directive>("AuthUserFile").Values[0];
            ac.ReadUserFile();
            }
            if (htaccess.GetCommand<Directive>("AuthName") != null)
            {
            ac.AuthName = (string)htaccess.GetCommand<Directive>("AuthName").Values[0];
            }
            if (htaccess.GetCommand<Directive>("AuthType") != null)
            {
            ac.AuthType = (string)htaccess.GetCommand<Directive>("AuthType").Values[0];
            ws._listener.AuthenticationSchemes = (AuthenticationSchemes)Enum.Parse(typeof(AuthenticationSchemes), ac.AuthType);
            HttpListenerContext context = ws._listener.GetContext();
            HttpListenerBasicIdentity identity = (HttpListenerBasicIdentity)context.User.Identity;
            if (identity != null)
            {
            foreach (var user in ac.Users)
            {
            if (user.Username == identity.Name)
            {
            if ((user.Password) == identity.Password)
            {
            if (File.Exists(f.ToString()))
            {
            // GetScriptByExtension(f.ToString());
            return File.ReadAllBytes(f.ToString());
            }
            else if (!request.Url.IsFile)
            {
            return File.ReadAllBytes(_wc.DataDir + "index.html");
            }
            else
            {
            return Encoding.ASCII.GetBytes(_wc.ErrorPages["403"]);
            }
            }
            else
            {
            return Encoding.ASCII.GetBytes(_wc.ErrorPages["403"]);
            }
            }
            }
            }
            else
            {
            return Encoding.ASCII.GetBytes(_wc.ErrorPages["404"]);
            }
            }
            }
            }
            else
            {
            if (request.Headers.AllKeys.ToList().Contains("service"))
            {
            if (request.IsSecureConnection)
            {
            }
            }
            }
            return null;
            }
            */
        }
    }
}