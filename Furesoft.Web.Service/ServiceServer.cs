using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Furesoft.Web.Service.Internal;

namespace Furesoft.Web.Service
{
    public class ServiceServer : HttpServer
    {
        public List<IService> Services = new List<IService>();

        public ServiceServer(int port) : base(IPAddress.Parse("127.0.0.1"), port)
        {
        }

        protected override void HandleGetRequest(HttpProcessor p)
        {
            var uri = new Uri("http://localhost/" + p.http_url);

            var service = GetService(uri.AbsolutePath);
            service.Args = Get.Create(p);

            var sres = service.Start();
            p.outputStream.Write(sres);

        }

        protected override void HandlePostRequest(HttpProcessor p, StreamReader inputData)
        {
            
        }

        private IService GetService(string name)
        {
            foreach (var s in Services)
            {
                if (s.Name == name)
                {
                    return s;
                }
            }
        }
    }
}