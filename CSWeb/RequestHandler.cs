using CSWeb.Configuration;
using Dalk.Web.HttpServer;
using System.IO;
using Xom;

namespace CSWeb
{
    public class RequestHandler : IRequestHandler
    {
        public void Configure(IniReader reader)
        {
            config = reader;
        }
        IniReader config;
        HttpRequest rqt = null;
        public void Handle(HttpRequest request, HttpResponse response)
        {
            rqt = request;
            var pth = config.Sections["Routing"].Values["domainwwwroot"].Value.Replace("{domain}", request.Headers["Host"]);
            if (!Directory.Exists(pth))
                pth = config.Sections["Routing"].Values["wwwroot"].Value;
            if (pth.TrimEnd().EndsWith("/"))
                pth = pth.Remove(pth.Length - 1);
            if (pth.TrimEnd().EndsWith("\\"))
                pth = pth.Remove(pth.Length - 1);
            pth = pth.Replace(":", "");
            var fullPath = pth + request.Path;
            var save = IsPathSave(fullPath, pth);
            byte[] bytes;
            XomInterfaceManager.GetRenderingPage(request.Path, out bool useXom, out IPluginPage page);
            if (useXom)
            {
                var builder = new XomBuilder(request.Path);
                page.BuildWebsite(builder);
                var bts = builder.ReadAll();
                bytes = bts;
            }
            else if (save)
            {
                bytes = GetFromFile(fullPath);
            }
            else
            {
                bytes = GetFromFile(pth);
            }
            response.ContentLenght = bytes.Length;
            response.Write(bytes);
            response.Send();
        }

        private byte[] GetFromFile(string path)
        {
            var pth = config.Sections["Routing"].Values["domainwwwroot"].Value.Replace("{domain}", rqt.Headers["Host"]);
            if (!Directory.Exists(pth))
                pth = config.Sections["Routing"].Values["wwwroot"].Value;
            if (pth.TrimEnd().EndsWith("/"))
                pth = pth.Remove(pth.Length - 1);
            if (pth.TrimEnd().EndsWith("\\"))
                pth = pth.Remove(pth.Length - 1);
            pth = pth.Replace(":", "");



            var fullFile = path;
            if (Directory.Exists(fullFile))
                if (File.Exists(fullFile + config.Sections["Routing"].Values["dirindex"].Value))
                    fullFile += config.Sections["Routing"].Values["dirindex"].Value;

            if (!File.Exists(fullFile))
            {
                if (File.Exists(pth + "/" + config.Sections["Routing"].Values["404"].Value))
                    return File.ReadAllBytes(pth + "/" + config.Sections["Routing"].Values["404"].Value);
                if (File.Exists(config.Sections["Routing"].Values["wwwroot"].Value + config.Sections["Routing"].Values["404"].Value))
                    return File.ReadAllBytes(config.Sections["Routing"].Values["wwwroot"].Value + config.Sections["Routing"].Values["404"].Value);
                else
                    return File.ReadAllBytes(config.Sections["Routing"].Values["404"].Value);
            }
            return File.ReadAllBytes(fullFile);
        }

        public static bool IsPathSave(string path, string domainPath)
        {
            if (Path.GetFullPath(path).StartsWith(Path.GetFullPath(domainPath)))
                return true;
            return false;
        }
    }
}
