using System.Collections.Generic;

namespace CSWeb.Configuration
{
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
}
