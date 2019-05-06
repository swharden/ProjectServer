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

        public override string GetHTML()
        {
            string html = "<h1>CELL</h1>";
            html += "<i>requires scanFolderFull</i>";
            return Html.BuildPage(html);
        }

        public override string GetText()
        {
            return "cell text";
        }
    }
}
