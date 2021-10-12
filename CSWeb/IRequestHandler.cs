using System.Net;

namespace CSWeb
{
    public interface IRequestHandler
    {
        void Handle(HttpListenerRequest request, HttpListenerResponse response, HttpListenerContext context);
        void Configure(Configuration.IniReader reader);
    }
}
