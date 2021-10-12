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
                WebServer ws = new(config);
                ws.Start();
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                Logger.Error("Restarting");
                Main(args);
            }
        }
    }
}
