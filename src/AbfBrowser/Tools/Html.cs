using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbfBrowser
{
    public static class Html
    {
        public static string Prettify(string html, int indentCount = 2, char indentChar = ' ')
        {
            int originalSize = html.Length;
            string indentString = String.Concat(Enumerable.Repeat(indentChar, indentCount));
            html = html.Replace(">", ">\n").Replace("<", "\n<");
            html = html.Replace("\r", "\n").Replace("\n\n", "\n");
            string[] lines = html.Split('\n');
            int indentLevel = -1;
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                if (line.StartsWith("<") && !line.StartsWith("</"))
                    indentLevel += 1;

                if (indentLevel > 0)
                {
                    int thisIndentLevel = indentLevel;
                    if (!line.StartsWith("<"))
                        thisIndentLevel += 1;
                    string thisIndent = String.Concat(Enumerable.Repeat(indentString, thisIndentLevel));
                    lines[i] = thisIndent + line;
                }

                if (line.StartsWith("</"))
                    indentLevel -= 1;

            }
            html = String.Join("\n", lines).Trim();
            html = html.Replace("\n", "\r\n");
            Debug.WriteLine($"prettified {originalSize} bytes of html (now {html.Length} bytes)");
            return html;
        }
    }
}
