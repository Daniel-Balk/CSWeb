using CSWeb.Configuration;
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
                var config = new IniReader();
                config.Read(File.ReadAllText("csweb.ini"));
                DllLoader.XomLoad();
                WebServer ws = new(config);
                ws.Start();
                DllLoader.StopXom();
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
