namespace CSWeb.Configuration
{
    public class IniValue
    {
        public IniValue()
        {
        }
        public string Raw { get; set; }
        public string Value
        {
            get
            {
                var respr = Raw;

                respr = respr
                    .Replace("\\\'", "\'")
                    .Replace("\\\'", "\"")
                    .Replace("\\:", ":")
                    .Replace("\\;", ";")
                    .Replace("\\\r", "\r")
                    .Replace("\\\n", "\n")
                    .Replace("\\\\", "\\");

                return respr;
            }
            set
            {
                var respr = value;

                respr = respr
                    .Replace("\'", "\\\'")
                    .Replace("\"", "\\\'")
                    .Replace(":", "\\:")
                    .Replace(";", "\\;")
                    .Replace("\r", "\\\r")
                    .Replace("\n", "\\\n")
                    .Replace("\\", "\\\\");

                Raw = respr;
            }
        }
        public string Key { get; set; }
        public override string ToString()
        {
            return Value;
        }
    }
}