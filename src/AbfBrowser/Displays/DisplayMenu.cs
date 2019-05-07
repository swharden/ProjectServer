using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AbfBrowser
{
    public class DisplayMenu : Display, IDisplay
    {
        public DisplayMenu(MessageResponse response) : base(response)
        {
            Debug.WriteLine($"Creating menu display from {response}");
        }

        private string HtmlNavigationCurrentFolder()
        {
            string html = "";
            html += "<div class='title2' style='margin-top: 20px;'>Current Folder</div>";
            string[] infoByFolder = Html.GetFolderPathsAndNamesBrokenUp(response.request.path);
            foreach (string line in infoByFolder)
            {
                string[] nameAndPath = line.Split(',');
                string name = nameAndPath[0];
                string path = nameAndPath[1];
                string url = $"?display=menu&path={path}";
                html += $"<a href='{url}' target='menu'>{name}</a>/";
            }
            return html;
        }

        private string HtmlNavigationSubFolders()
        {
            string html = "";
            foreach (string folderName in response.AbfFolder.GetFolderNames())
            {
                if (Configuration.analysisFolderNames.Contains(folderName))
                    continue;
                string url = $"?display=menu&path={response.request.path}/{folderName}";
                html += $"<div style='margin-left: 10px;'><a href='{url}' target='menu'>{folderName}</a></div>";
            }
            return html;
        }

        private string HtmlForAbfList()
        {
            if (response.AbfFolder.parentsAndChildren.Count == 0) {
                Debug.WriteLine("no parents/children found so not showing ABFs in menu");
                return "";
            }

            string html = "";
            html += "<div class='title2' style='margin-top: 20px;'>Parent ABFs</div>";
            foreach (string parent in response.AbfFolder.parentsAndChildren.Keys)
            {
                string parentID = System.IO.Path.GetFileNameWithoutExtension(parent);
                string url = $"?display=cell&path={response.request.path}&identifier={parent}";
                string comment = response.AbfFolder.abfNotes.GetComment(parentID);
                string colorHex = response.AbfFolder.abfNotes.GetColorHex(parentID);
                html += $"<div>";
                html += $"<a class='menuParent' href='{url}' target='content' style='background-color: {colorHex}'>{parentID}</a> ";
                html += $"</span>";
                html += $"<span class='menuComment'>{comment}</style>";
                html += $"</div>";
            }

            return html;
        }

        public override string GetHTML()
        {
            string html = "";
            html += $"<div class='menuBody'>";
            html += "<div class='title'>ABF Browser</div>";
            html += HtmlNavigationCurrentFolder();
            html += HtmlNavigationSubFolders();
            html += HtmlForAbfList();
            html += "</div>";
            return Html.BuildPage(html);
        }

        public override string GetText()
        {
            return "menu text";
        }
    }
}
