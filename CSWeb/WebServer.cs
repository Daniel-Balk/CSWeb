using CSWeb.Configuration;
using Dalk.Web.HttpServer;
using Logging.Net;
using System;
using System.Collections.Generic;
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
            XomInterfaceManager.Pages.Clear();
            var register = new XomInterfaceManager();
            foreach (var xomPlugin in DllLoader.PLUGINS)
            {
                xomPlugin.RegistryRoutes(register);
            }
            
            HttpListener listener = new(Port);
            Logger.Info("Initialized Listener");
            listener.Start();
            Logger.Info("Listener started");
            IRequestHandler rh = new RequestHandler();
            rh.Configure(Config);
            while (listen)
            {
                if (solve)
                {
                    var request = listener.AcceptRequest();
                    Logger.Info("AccessLog: Request by " + request.GetSender().Client.RemoteEndPoint + " for " + request.Headers["Host"]);
                    var response = request.GetResponse();
                    Thread t = null;
                    t = new Thread(new ThreadStart(() =>
                    {
                        rh.Handle(request, response);
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
