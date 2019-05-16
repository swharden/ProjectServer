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

        private string HtmlForAbfList_ignoreCellsFile()
        {
            if (response.AbfFolder.parentsAndChildren.Count == 0)
            {
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

        private string HtmlMenuLinkForAbf(AbfInfo info)
        {
            string url = $"?display=cell&path={response.request.path}&identifier={info.abfID}.abf";
            string comment = response.AbfFolder.abfNotes.GetComment(info.abfID);
            string colorHex = response.AbfFolder.abfNotes.GetColorHex(info.abfID);
            string html = "";
            html += $"<div>";
            html += $"<a class='menuParent' href='{url}' target='content' style='background-color: {colorHex}'>{info.abfID}</a> ";
            html += $"<span class='menuComment'>{comment}</span>";
            html += $"</div>";
            return html;
        }
        private string HtmlMenuLinkForGroup(AbfInfo info)
        {
            string html = "";
            html += $"<div style='font-weight: bold;'><br>";
            html += $"{info.group}";
            html += $"</div>";
            return html;
        }

        private string HtmlForAbfList()
        {
            if (response.AbfFolder.parentsAndChildren.Count == 0)
            {
                Debug.WriteLine("no parents/children found so not showing ABFs in menu");
                return "";
            }

            string html = "";
            html += "<div class='title2' style='margin-top: 20px;'>Parent ABFs</div>";

            List<string> abfIdsDisplayed = new List<string>();
            foreach (AbfInfo info in response.AbfFolder.abfNotes.GetAbfInfo())
            {
                if (info.abfID == "---")
                    html += HtmlMenuLinkForGroup(info);
                else
                    html += HtmlMenuLinkForAbf(info);
                abfIdsDisplayed.Add(info.abfID);
            }

            List<string> ungroupedAbfs = new List<string>();
            foreach (string parent in response.AbfFolder.parentsAndChildren.Keys)
                if (!abfIdsDisplayed.Contains(System.IO.Path.GetFileNameWithoutExtension(parent)))
                    ungroupedAbfs.Add(parent);

            if (ungroupedAbfs.Count() > 0)
            {
                html += HtmlMenuLinkForGroup(new AbfInfo());
                foreach (string parent in response.AbfFolder.parentsAndChildren.Keys)
                    if (!abfIdsDisplayed.Contains(parent))
                        html += HtmlMenuLinkForAbf(new AbfInfo(parent));
            }


            return html;
        }

        public string HtmlMenuTitle()
        {
            const string url = "http://github.com/swharden/ABF-browser";
            string html = "";
            html += "<div>";
            html += "<span class='title'>ABF Browser</span> ";
            html += $"<span class='menuComment'><a href='{url}' target='_blank' style='color: #666;'>v1.2</a></span>";
            html += "</div>";
            return html;
        }

        public override string GetHTML()
        {
            string html = "";
            html += $"<div class='menuBody'>";
            html += HtmlMenuTitle();
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
