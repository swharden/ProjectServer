using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;

namespace AbfBrowser
{
    class DisplayFrames : Display, IDisplay
    {
        public DisplayFrames(MessageResponse response) : base(response)
        {
            Debug.WriteLine($"Creating frames display from {response}");
        }

        public override string GetHTML()
        {
            List<string> queries = new List<string>();
            string html = Properties.Resources.frames;
            string path = response.request.path;
            html = html.Replace("~URL1~", $"?display=menu&path={path}");
            html = html.Replace("~URL2~", $"?display=cell&path={path}");
            return html;
        }

        public override string GetText()
        {
            return "home text";
        }
    }
}
