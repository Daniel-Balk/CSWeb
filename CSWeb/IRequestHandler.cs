using Dalk.Web.HttpServer;

namespace CSWeb
{
    public interface IRequestHandler
    {
        void Handle(HttpRequest request, HttpResponse response);
        void Configure(Configuration.IniReader reader);
    }
}
