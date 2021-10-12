using System.Collections.Generic;
using Xom;

namespace CSWeb
{
    public class XomInterfaceManager : IRoutingRegistryManager
    {
        public static List<IPluginPage> Pages { get; set; } = new();
        public void Register(IPluginPage page)
        {
            Pages.Add(page);
        }
        public static void GetRenderingPage(string route, out bool success, out IPluginPage page)
        {
            foreach (var v in Pages)
            {
                if (v.GetRoute().ApplyCondition(route))
                {
                    page = v;
                    success = true;
                    return;
                }
            }
            success = false;
            page = null;
        }
    }
}