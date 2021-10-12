using System;
using Xom;

namespace XomTesting
{
    public class Plugin : IPlugin
    {
        public void PluginSetup()
        {
        }

        public void RegistryRoutes(IRoutingRegistryManager routing)
        {
            routing.Register(new MyPage());
        }

        public void Shutdown()
        {
            
        }
    }
}
