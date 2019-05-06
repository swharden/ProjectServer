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

        public string GetMenuHtml()
        {
            string html = "<div><b>MENU</b></div>";
            foreach (string parent in response.AbfFolder.parentsAndChildren.Keys)
            {
                html += $"<div><b>{parent}</b></div>";
                foreach(string child in response.AbfFolder.parentsAndChildren[parent])
                {
                    html += $"<div>{child}</div>";
                }
            }
            return html;
        }

        public override string GetHTML()
        {
            string html = "";
            html += "<html><body>";
            html += GetMenuHtml();
            html += "</body></html>";
            html = Html.Prettify(html);
            return html;
        }

        public override string GetText()
        {
            return "menu text";
        }
    }
}
