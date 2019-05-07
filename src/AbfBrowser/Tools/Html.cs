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
        public static string ColorHexToRgba(string hex, double alpha = 0.25)
        {
            hex = hex.Replace("#", "");
            if (hex.Length == 3)
                hex = hex + hex;
            if (hex.Length != 6)
                throw new Exception("hex color code must be 6 characters");
            int rgb = Convert.ToInt32(hex, 16);
            byte r = (byte)(rgb >> 16);
            byte g = (byte)(rgb >> 8);
            byte b = (byte)(rgb >> 0);
            return $"rgba({r}, {g}, {b}, {alpha})";
        }

        public static string BuildPage(string bodyHtml, string bottomMessage = "", string title = "ABF Browser")
        {
            string topHtml = Properties.Resources.top;
            topHtml = topHtml.Replace("~TITLE~", title);
            topHtml = topHtml.Replace("~CSS~", Properties.Resources.style);
            string bottomHtml = Properties.Resources.bot;
            if (bottomMessage.Length > 0)
                bottomHtml = $"<div class='bottomMessage'>{bottomMessage} ~SERVER_NOTES~</div>{bottomHtml}";
            return topHtml + bodyHtml + bottomHtml;
        }

        public static string[] GetFolderPathsAndNamesBrokenUp(string inputPath)
        {
            string driveLetter = System.IO.Path.GetPathRoot(inputPath).TrimEnd(new char[] { '/', '\\' });
            List<string> paths = new List<string>();
            List<string> names = new List<string>();
            while (inputPath != null)
            {
                paths.Add(inputPath);
                names.Add(System.IO.Path.GetFileName(inputPath));
                inputPath = System.IO.Path.GetDirectoryName(inputPath);
            }
            paths.Reverse();
            names.Reverse();
            names[0] = driveLetter;

            string[] folderLinks = new string[paths.Count];
            for (int i = 0; i < paths.Count; i++)
                folderLinks[i] = $"{names[i]},{paths[i]}";
            return folderLinks;
        }

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
