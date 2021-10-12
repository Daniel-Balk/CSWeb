using System.Collections.Generic;

public class IniKeyValueCollection : Dictionary<string, IniValue>
{
}
public class IniReader
{
    public void Read(string ini)
    {
        List<string> sectionStrings = new();
        List<int> sectionStartLines = new();

        var y = ini.Replace("\r", "");
        int i = 0;

        var lines = y.Split('\n');
        foreach (var line in lines)
        {
            if (line.Trim().StartsWith("[") && line.TrimEnd().EndsWith("]"))
            {
                sectionStartLines.Add(i);
            }
            i++;
        }
        int k = 0;
        foreach (var j in sectionStartLines)
        {
            int m = lines.Length;
            if (k + 1 <= sectionStartLines.Count - 1)
            {
                m = sectionStartLines[k + 1];
            }
            string section = "";
            for (int l = j; l < m; l++)
            {
                section += lines[l] + "\n";
            }
            sectionStrings.Add(section);
            k++;
        }

        foreach (var v in sectionStrings)
        {
            IniSection s = new();
            s.ParseSection(v);
            Sections[s.Name] = s;
        }
    }
    public IniSectionCollection Sections { get; private set; } = new();
}
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
            else if (string.IsNullOrEmpty(line.Trim()))
            {
                // empty line
                // --> ignore
            }
            else
            {
                try
                {
                    IniValue value = new IniValue();

                    var key = line.Split("=")[0].Trim().TrimEnd();
                    var vl = line.Remove(0, key.Length + 1);

                    value.Key = key;
                    value.Value = vl;

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
public class IniSectionCollection : Dictionary<string, IniSection>
{
}
public class IniValue
{
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

            Raw = value;
        }
    }
    public string Key { get; set; }
    public override string ToString()
    {
        return Value;
    }
}