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

        private string GetHtmlFolderSummary()
        {
            string html = $"<h1>FOLDER SUMARY</h1>";
            html += $"<div><code>{response.request.path}</code></div>";
            html += "<hr>";

            string[] parents = response.AbfFolder.parentsAndChildren.Keys.ToArray();
            foreach (string parent in parents)
            {
                string parentID = System.IO.Path.GetFileNameWithoutExtension(parent);
                string url = $"?display=cell&path={response.request.path}&identifier={parent}";
                html += $"<br><div><b><a href='{url}'>{parentID}</a></b></div>";
                string[] children = response.AbfFolder.parentsAndChildren[parent];
                foreach (string child in children) {
                    html += $"<div>{child}</div>";
                }
            }
            return html;
        }

        private string GetHtmlParentSummary()
        {
            string parent = response.request.identifier;
            string html = $"<h1>CELL {parent}</h1>";
            html += $"<div><code>{response.request.path}/{parent}</code></div>";

            string[] children = response.AbfFolder.parentsAndChildren[parent];
            foreach (string child in children)
            {
                string childID = System.IO.Path.GetFileNameWithoutExtension(child);
                if (response.AbfFolder.analysisFolder != null)
                {
                    foreach (string fname in response.AbfFolder.analysisFolder.GetFileNames())
                    {
                        if (fname.StartsWith(childID))
                        {
                            string url = response.AbfFolder.analysisFolder.path + "/" + fname;
                            url = url.Replace("\\", "/");
                            url = "/fs/" + url;
                            html += $"<a href='{url}'><img src='{url}' width='300'></a> ";
                        }
                    }
                }
            }
            return html;
        }

        public override string GetHTML()
        {
            string html;

            if (response.request.identifier == null)
                html = GetHtmlFolderSummary();
            else
                html = GetHtmlParentSummary();

            return Html.BuildPage(html);
        }

        public override string GetText()
        {
            return "cell text";
        }
    }
}
