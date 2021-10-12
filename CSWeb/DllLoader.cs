using System.IO;

namespace CSWeb
{
    public class DllLoader
    {
        public static void XomLoad()
        {
            if (!Directory.Exists("xom"))
                Directory.CreateDirectory("xom");
        }
    }
}
