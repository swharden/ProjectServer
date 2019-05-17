using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AbfBrowser
{
    class DisplayLabtalk : Display, IDisplay
    {

        public DisplayLabtalk(MessageResponse response) : base(response)
        {
            Debug.WriteLine($"Creating labtalk display from {response}");
        }

        public override string GetHTML()
        {
            string ltCommand = Html.UrlDecode(response.request.value);
            string html = $"Executing LabTalk:<br><code>{ltCommand}</code>";
            return Html.BuildPage(html, response.GetStatus());
        }

        public override string GetText()
        {
            return "labtalk text";
        }
    }
}
