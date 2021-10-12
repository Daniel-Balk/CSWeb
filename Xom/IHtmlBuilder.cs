namespace Xom
{
    public interface IHtmlBuilder
    {
        void Write(string str);
        void Write(byte[] bytes);
        void Write(object o);
        string ReadAll();
        string GetPath();
    }
}