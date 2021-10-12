using System.IO;
using System.Text;
using Xom;

namespace CSWeb
{
    public class XomBuilder : IHtmlBuilder
    {
        private readonly string route;
        private byte[] bytes = System.Array.Empty<byte>();

        public XomBuilder(string route)
        {
            this.route = route;
        }

        public string GetPath()
        {
            return route;
        }

        public byte[] ReadAll()
        {
            return bytes;
        }

        public void Write(string str)
        {
            Write(Encoding.UTF8.GetBytes(str));
        }

        public void Write(byte[] bytes)
        {
            MemoryStream ms = new();
            ms.Write(this.bytes, 0, this.bytes.Length);
            ms.Write(bytes, 0, bytes.Length);
            this.bytes = ms.ToArray();
            ms.Close();
        }

        public void Write(object o)
        {
            Write(o.ToString());
        }
    }
}