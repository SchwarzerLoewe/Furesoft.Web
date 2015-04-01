using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Furesoft.Web.Service.Internal
{
    public abstract class HttpServer
    {
        private readonly IPAddress address;

        protected int port;
        TcpListener listener;
        bool is_active = true;

        public HttpServer(IPAddress address, int port)
        {
            this.address = address;
            this.port = port;
        }

        public void Listen()
        {
            listener = new TcpListener(new IPEndPoint(address, port));
            listener.Start();

            while (is_active)
            {
                TcpClient s = listener.AcceptTcpClient();
                HttpProcessor processor = new HttpProcessor(s, this);
                Thread thread = new Thread(new ThreadStart(processor.process));
                thread.Start();
                Thread.Sleep(1);
            }
        }

        protected abstract void HandleGetRequest(HttpProcessor p);

        protected abstract void HandlePostRequest(HttpProcessor p, StreamReader inputData);
    }
}