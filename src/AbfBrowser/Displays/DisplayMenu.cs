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

        private string HtmlForFolderMenu()
        {
            string html = "<h2>Current Folder:</h2>";
            string[] infoByFolder = Html.GetFolderPathsAndNamesBrokenUp(response.request.path);
            foreach (string line in infoByFolder)
            {
                string[] nameAndPath = line.Split(',');
                string name = nameAndPath[0];
                string path = nameAndPath[1];
                string url = $"?display=menu&path={path}";
                html += $"<a href='{url}' target='menu'>{name}</a>/";
                Console.WriteLine(nameAndPath);
            }

            html += "<h2>Sub-Folders:</h2>";
            foreach (string folderName in response.AbfFolder.GetFolderNames())
            {
                string url = $"?display=menu&path={response.request.path}/{folderName}";
                html += $"<div><a href='{url}' target='menu'>{folderName}</a></div>";
            }

            html = $"<div style='margin-bottom: 20px;'>{html}</div>";
            return html;
        }

        private string HtmlForAbfList()
        {
            if (response.AbfFolder.parentsAndChildren.Count == 0) {
                Debug.WriteLine("no parents/children found so not showing ABFs in menu");
                return "";
            }

            string html = "";
            html += "<h2>ABF Parents:</h2>";
            foreach (string parent in response.AbfFolder.parentsAndChildren.Keys)
            {
                string parentID = System.IO.Path.GetFileNameWithoutExtension(parent);
                string url = $"?display=cell&path={response.request.path}&identifier={parent}";
                html += $"<div><a href='{url}' target='content'>{parentID}</a></div>";
            }

            return html;
        }

        public override string GetHTML()
        {
            string html = "";
            html += "<h1>MENU</h1>";
            html += HtmlForFolderMenu();
            html += HtmlForAbfList();

            html = $"<div class='bodyMenu'>{html}</div>";
            return Html.BuildPage(html);
        }

        public override string GetText()
        {
            return "menu text";
        }
    }
}
