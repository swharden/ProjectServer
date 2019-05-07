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
            string html = "";

            html += $"<div style='margin-bottom: 50px;'>";
            html += $"<h1>FOLDER SUMARY</h1>";
            html += $"<div>{response.request.path}</div>";
            html += $"</div>";

            string[] parents = response.AbfFolder.parentsAndChildren.Keys.ToArray();
            foreach (string parent in parents)
            {
                string parentID = System.IO.Path.GetFileNameWithoutExtension(parent);
                string url = $"?display=cell&path={response.request.path}&identifier={parent}";
                string[] children = response.AbfFolder.parentsAndChildren[parent];
                string comment = response.AbfFolder.abfNotes.GetComment(parentID);
                string colorHex = response.AbfFolder.abfNotes.GetColorHex(parentID);
                string colorHexTransparent = Html.ColorHexToRgba(colorHex);

                html += $"<div style='margin: 10px; border: 3px solid {colorHex};'>";

                html += $"<div style='background-color: {colorHex}; font-size: 150%;'>";
                html += $"<span style='padding: 5px; font-weight: bold;'><a href='{url}'>{parentID}</a></span> ";
                html += $"<span style='padding: 5px; font-style: italic;'>{comment}</span>";
                html += $"</div>";

                html += $"<div style='padding: 5px; background-color: {colorHexTransparent};'>";
                foreach (string child in children)
                    html += $"<div>{child}</div>";
                html += "</div>";

                html += $"</div>";
            }
            return html;
        }
        
        private string HtmlParentSummaryTopper()
        {
            string html = "";

            string parent = response.request.identifier;
            string parentID = System.IO.Path.GetFileNameWithoutExtension(parent);
            string[] children = response.AbfFolder.parentsAndChildren[parent];
            string comment = response.AbfFolder.abfNotes.GetComment(parentID);
            string colorHex = response.AbfFolder.abfNotes.GetColorHex(parentID);
            string colorHexTransparent = Html.ColorHexToRgba(colorHex);

            // title area start
            html += $"<div style='background-color: {colorHex}; padding: 0px 10px 10px 10px;'>"; // color
            html += $"<div class='title'>{parentID}</div>";
            html += $"<form style='margin: 0px;'>";

            // comment
            html += $"<div style='display:inline-block; padding-right: 5px;'>";
            html += $"<span class='cellCommentLabel'>comment:</span>";
            html += $"<input class='inputComment' type='text' name='comment' value='burst, resonant 5x' placeholder='write comment' />";
            html += $"</div>";

            // color
            html += $"<div style='display:inline-block; padding-right: 5px;'>";
            html += $"<span class='cellCommentLabel'>color:</span>";
            html += $"<select class='selectColor'>";
            foreach (string thisColorCode in Configuration.ColorsByCode.Keys)
            {
                string thisColorHex = Configuration.ColorsByCode[thisColorCode];
                string thisColorName = thisColorCode;
                if (thisColorName == "")
                    thisColorName = "none";
                html += $"<option value='{thisColorHex}' style='background-color: {thisColorHex};'>{thisColorName}</option>";
            }
            html += $"</select>";
            html += $"</div>";

            // group
            html += $"<div style='display:inline-block; padding-right: 5px;'>";
            html += $"<span class='cellCommentLabel'>group:</span>";
            string[] groups = new string[] { "ungrouped", "Pyramidal", "Interneuron" };
            html += $"<select class='selectColor'>";
            foreach (string group in groups)
            {
                html += $"<option value='{group}'>{group}</option>";
            }
            html += $"</select>";
            html += $"</div>";

            // save
            html += $"<div style='display:inline-block; padding-right: 5px;'>";
            html += $"<input class='btnCommentSave' type='submit' value='save'/>";
            html += $"</div>";

            // title area end
            html += $"</form>";
            html += $"</div>"; // color
            html += $"";

            // children
            html += $"<div style='background-color: {colorHexTransparent}; padding: 10px;'>";
            Random rand = new Random();
            foreach (string child in children)
            {
                string protocol = Configuration.GetRandomKnownProtocol(rand);
                double randomFileSize = Math.Round(rand.NextDouble() * 100, 2);
                string fileSizeString = $"{randomFileSize} MB";
                string commentsString = "";
                if (rand.NextDouble() > .8)
                    commentsString = "\"+DNQX\" @ 5:43; \"+AP5\" @ 6:32";

                html += $"<div>";
                html += $"<span style=''>{child}</span> ";
                html += $"<button class='btnSmall'>copy</button> ";
                html += $"<button class='btnSmall'>setpath</button> ";
                html += $"<span class='abfLineInfo'>";
                html += $"{protocol}, ";
                html += $"{fileSizeString}, ";
                html += $"{commentsString}";
                html += $"</span>";
                html += $"</div>";
            }
            html += $"</div>";

            return html;
        }

        private string HtmlParentSummaryImages()
        {
            string html = "";
            string parent = response.request.identifier;
            string[] children = response.AbfFolder.parentsAndChildren[parent];

            html += $"<div style='padding: 10px; margin-top: 20px;'>";
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
                            html += $"<a href='{url}'><img src='{url}' class='imgThumbnail'></a> ";
                        }
                    }
                }
            }
            html += $"</div>";
            return html;
        }

        private string GetHtmlParentSummary()
        {
            string html = "";
            html += HtmlParentSummaryTopper();
            html += HtmlParentSummaryImages();
            return html;
        }

        public override string GetHTML()
        {
            string html;

            if (response.request.identifier == null)
                html = GetHtmlFolderSummary();
            else
                html = GetHtmlParentSummary();
            return Html.BuildPage(html, response.GetStatus());
        }

        public override string GetText()
        {
            return "cell text";
        }
    }
}
