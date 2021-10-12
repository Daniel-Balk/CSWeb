namespace Xom
{
    public interface IPluginPage
    {
        IRoute GetRoute();
        void BuildWebsite(IHtmlBuilder builder);
    }
}