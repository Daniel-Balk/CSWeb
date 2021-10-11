using System;

namespace CSWeb.Configuration
{
    public class IniSection
    {
        public void ParseSection(string section)
        {
            var s = section.Replace("\r", "");
            var lines = s.Split('\n');
            foreach (var line in lines)
            {
                if (line.Trim().TrimEnd().StartsWith("[") && line.Trim().TrimEnd().EndsWith("]"))
                {
                    var t = line.Remove(0, 1);
                    Name = t.Remove(t.Length - 1);
                }
                else if (line.Trim().TrimEnd().StartsWith("#"))
                {
                    // this is a comment
                    // --> ignoring
                }
                else if (line.Trim().TrimEnd().StartsWith(";"))
                {
                    // this is a comment
                    // --> ignoring
                }
                else if(string.IsNullOrEmpty(line.Trim()))
                {
                    // empty line
                    // --> ignore
                }
                else
                {
                    try
                    {
                        IniValue value = new();

                        var key = line.Split("=")[0].Trim().TrimEnd();
                        var vl = line.Remove(0, key.Length + 1);

                        value.Key = key;
                        value.Value = vl;

                        Values[value.Key] = value;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }
        public IniKeyValueCollection Values { get; set; } = new();
        public string Name { get; set; }
    }
}