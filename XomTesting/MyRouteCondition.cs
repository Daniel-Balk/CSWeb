using Xom;

namespace XomTesting
{
    internal class MyRouteCondition : IRoute
    {
        public bool ApplyCondition(string route)
        {
            if (route == "/xom")
                return true;
            return false;
        }
    }
}