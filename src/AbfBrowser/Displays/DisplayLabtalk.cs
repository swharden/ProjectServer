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

        public void ExecuteLabTalk(string ltCommand)
        {
            Debug.WriteLine($"connecting to Origin instance...");
            Origin.ApplicationSI origin = new Origin.ApplicationSIClass();

            Debug.WriteLine($"focusing and maximizing OriginLab...");
            origin.Visible = Origin.MAINWND_VISIBLE.MAINWND_SHOW_BRING_TO_FRONT;
            origin.Visible = Origin.MAINWND_VISIBLE.MAINWND_SHOWMAXIMIZED;

            Debug.WriteLine($"Executing labtalk command [{ltCommand}]...");
            origin.Execute(ltCommand);

            Debug.WriteLine($"disconnecting from Origin...");
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(origin);
            Debug.WriteLine($"Origin instance released.");
        }

        public override string GetHTML()
        {
            string ltCommand = Html.UrlDecode(response.request.value);
            string html = $"Executing LabTalk:<br><code>{ltCommand}</code>";
            ExecuteLabTalk(ltCommand);
            return Html.BuildPage(html, response.GetStatus());
        }

        public override string GetText()
        {
            return "labtalk text";
        }
    }
}
