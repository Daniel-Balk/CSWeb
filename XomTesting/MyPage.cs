using Xom;

namespace XomTesting
{
    public class MyPage : IPluginPage
    {
        public void BuildWebsite(IHtmlBuilder builder)
        {
            builder.Write("<p>This site was loaded by <b>XOM</b></p>");
        }

        public IRoute GetRoute()
        {
            return new MyRouteCondition();
        }
    }
}