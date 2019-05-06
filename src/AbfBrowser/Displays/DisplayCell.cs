using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AbfBrowser
{
    public class DisplayCell : Display, IDisplay
    {
        public DisplayCell(MessageResponse response) : base(response)
        {
            Debug.WriteLine($"Creating cell display from {response}");
        }

        public override string GetHTML()
        {
            string parent = response.request.identifier;
            string[] children = response.AbfFolder.parentsAndChildren[parent];

            string html = $"<h1>CELL {parent}</h1>";
            foreach (string child in children)
            {
                string childID = System.IO.Path.GetFileNameWithoutExtension(child);
                foreach(string fname in response.AbfFolder.analysisFolder.GetFileNames())
                {
                    if (fname.StartsWith(childID))
                    {
                        string url = response.AbfFolder.analysisFolder.path + "/" + fname;
                        url = url.Replace("\\", "/");
                        url = url.Replace(Configuration.dataRootPath, Configuration.dataRootPathWebAlias);
                        html += $"<a href='{url}'><img src='{url}' width='300'></a> ";
                    }
                }
            }

            string debugMessage = $"Total request-to-page time: {response.request.elapsedMillisecString} milliseconds";
            html += $"<div style='margin-top: 50px; color: #DDD;'><code>{debugMessage}</code></div>";
            return Html.BuildPage(html);
        }

        public override string GetText()
        {
            return "cell text";
        }
    }
}
