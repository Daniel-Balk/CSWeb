using CSWeb.Configuration;
using Logging.Net;
using System;
using System.IO;

namespace CSWeb
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                CwMan.Constant.GoIn();
                var config = new IniReader();
                config.Read(File.ReadAllText("csweb.ini"));
                DllLoader.XomLoad();
                WebServer ws = new(config);
                ws.Start();
                DllLoader.StopXom();
                CwMan.Constant.GoOut();
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                Logger.Error("Restarting");
                DllLoader.StopXom();
                Main(args);
            }
        }
    }
}
