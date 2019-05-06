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
            string html = Properties.Resources.home;
            return html;
        }

        public override string GetText()
        {
            return "home text";
        }
    }

}
