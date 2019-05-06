using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AbfBrowser
{
    public class DisplayHome : Display, IDisplay
    {
        public DisplayHome(MessageResponse response) : base(response)
        {
            Debug.WriteLine($"Creating home display from {response}");
        }

        public override string GetHTML()
        {
            List<string> queries = new List<string>();

            queries.Add(@"/");
            queries.Add(@"?action=scanFolderFast&path=D:\demoData\abfs-2019");
            queries.Add(@"?action=scanFolderFast");
            queries.Add(@"?action=alsdjfahksdhfkajshdf");

            string html = "";
            html += "<html><body>";
            html += "<h1>HOME</h1>";
            foreach (string query in queries)
                html += $"<li><a href='{query}'>{query}</a></li>";
            html += "</body></html>";
            html = Html.Prettify(html);
            return html;
        }

        public override string GetText()
        {
            return "home text";
        }
    }

}
