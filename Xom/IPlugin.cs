namespace Xom
{
    public interface IPlugin
    {
        void PluginSetup();
        void Shutdown();
        void RegistryRoutes(IRoutingRegistryManager routing);
    }
}