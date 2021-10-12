using CSWeb.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CSWeb
{
    public class RequestHandler : IRequestHandler
    {
        public void Configure(IniReader reader)
        {
            config = reader;
        }
        IniReader config;
        public void Handle(HttpListenerRequest request, HttpListenerResponse response, HttpListenerContext context)
        {
            var pth = config.Sections["Routing"].Values["domainwwwroot"].Value.Replace("{domain}", request.UserHostAddress.Split(':')[0]);
            if (!Directory.Exists(pth))
                pth = config.Sections["Routing"].Values["wwwroot"].Value;
            if (pth.TrimEnd().EndsWith("/"))
                pth = pth.Remove(pth.Length - 1);
            if (pth.TrimEnd().EndsWith("\\"))
                pth = pth.Remove(pth.Length - 1);
            pth = pth.Replace(":", "");
            var fullPath = pth + request.RawUrl;
            var save = IsPathSave(fullPath, pth);
            byte[] bytes;
            if (save)
            {
                bytes = GetFromFile(fullPath);
            }
            else
            {
                bytes = GetFromFile(pth);
            }
            // Get a response stream and write the response to it.
            response.ContentLength64 = bytes.Length;
            Stream output = response.OutputStream;
            output.Write(bytes, 0, bytes.Length);
            // You must close the output stream.
            output.Close();
        }

        private byte[] GetFromFile(string path)
        {

            var fullFile = path;
            if (Directory.Exists(fullFile))
                if (File.Exists(fullFile + config.Sections["Routing"].Values["dirindex"].Value))
                    fullFile += config.Sections["Routing"].Values["dirindex"].Value;

            if (!File.Exists(fullFile))
                return File.ReadAllBytes(config.Sections["Routing"].Values["404"].Value);
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
