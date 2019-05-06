using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AbfBrowser
{
    public class DisplayError : Display, IDisplay
    {
        public DisplayError(MessageResponse response) : base(response)
        {
            Debug.WriteLine($"Creating error display from {response}");
        }

        public override string GetHTML()
        {
            string html = "<h1>ERROR</h1>";
            return Html.BuildPage(html);
        }

        public override string GetText()
        {
            return "error text";
        }
    }
}
