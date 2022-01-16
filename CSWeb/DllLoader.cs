using Logging.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xom;

namespace CSWeb
{
    public class DllLoader
    {
        public readonly static List<Assembly> XOM_ASSEMBLYS = new();
        public readonly static List<IPlugin> PLUGINS = new();
        public static void XomLoad()
        {
            if (!Directory.Exists("xom"))
                Directory.CreateDirectory("xom");
            foreach (var xll in Directory.GetFiles("xom","*.dll"))
            {
                Logger.Warn("Found XOM Extension: " + xll);
                try
                {
                    var a = Assembly.LoadFrom(xll);
                    XOM_ASSEMBLYS.Add(a);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }
            }
            foreach (var asm in XOM_ASSEMBLYS)
            {
                var plugin = XomPlug(asm);
                XomEnable(plugin);
            }
        }
        private static void XomEnable(IPlugin plugin)
        {
            plugin.PluginSetup();
            PLUGINS.Add(plugin);
        }
        public static void StopXom()
        {
            foreach (var l in PLUGINS)
            {
                l.Shutdown();
            }
            PLUGINS.Clear();
            XOM_ASSEMBLYS.Clear();
        }
        private static IPlugin XomPlug(Assembly asm)
        {
            foreach (var tp in asm.GetTypes())
            {
                try
                {
                    var o = Activator.CreateInstance(tp);
                    var p = (IPlugin)o;
                    return p;
                }
                catch (Exception)
                {

                }
            }
            return Activator.CreateInstance<IPlugin>();
        }
    }
}
