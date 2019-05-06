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

        public override string GetHTML()
        {
            string html = "<h1>MENU</h1>";
            foreach (string parent in response.AbfFolder.parentsAndChildren.Keys)
            {
                html += $"<br><div><b>{parent}</b></div>";
                foreach (string child in response.AbfFolder.parentsAndChildren[parent])
                    html += $"<div>{child}</div>";
            }
            return Html.BuildPage(html);
        }

        public override string GetText()
        {
            return "menu text";
        }
    }
}
