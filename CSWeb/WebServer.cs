using CSWeb.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace CSWeb
{
    public class WebServer : IDisposable
    {
        readonly List<IDisposable> disposing = new();
        public string Adress { get; set; } = "localhost";
        public int Port { get; set; } = 58080;
        public IniReader Config { get; }

        private bool listen = true;
        private bool solve = true;

        public WebServer(IniReader config)
        {
            Config = config;
            Adress = Config.Sections["Server"].Values["Adress"].Value;
            try
            {
                Port = int.Parse(Config.Sections["server"].Values["Adress"].Value);
            }
            catch (Exception)
            {

            }
        }

        public void Start()
        {
            var prefixes = new string[] { "http://" + Adress + ":" + Port + "/" };
            if (!HttpListener.IsSupported)
            {
                Logger.Error("HttpListener class is unsupported!");
                return;
            }
            else
            {
                Logger.Info("Starting HTTP Webserver on " + Port);
            }
            if (prefixes == null || prefixes.Length == 0)
            {
                Logger.Error("No Server/Port; Aborting");
                return;
            }
            HttpListener listener = new();
            Logger.Info("Initialized Listener");
            foreach (string s in prefixes)
            {
                listener.Prefixes.Add(s);
            }
            listener.Start();
            Logger.Info("Listener started");
            disposing.Add(listener);
            IRequestHandler rh = new RequestHandler();
            rh.Configure(Config);
            while (listen)
            {
                if (solve)
                {
                    HttpListenerContext context = listener.GetContext();
                    HttpListenerRequest request = context.Request;
                    Logger.Info("AccessLog: Request by " + request.RemoteEndPoint.Address + " for " + request.UserHostAddress);
                    HttpListenerResponse response = context.Response;
                    Thread t = null;
                    t = new Thread(new ThreadStart(() =>
                    {
                        rh.Handle(request, response, context);
                        t.Interrupt();
                    }));
                    t.Start();
                }
            }
            listener.Stop();
        }
        public void Stop()
        {
            listen = false;
        }
        public void Pause()
        {
            solve = false;
        }
        public void Resume()
        {
            solve = false;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            disposing.ForEach((dispose) =>
            {
                try
                {
                    dispose.Dispose();
                }
                catch (Exception)
                {

                }
            });
            GC.Collect();
        }
    }
}
