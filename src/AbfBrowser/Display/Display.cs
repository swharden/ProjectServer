using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AbfBrowser
{
    public interface IDisplay
    {
        string GetHTML();

        string GetText();
    }

    public abstract class Display
    {
        public Dictionary<string, string> items;

        public Display(string json)
        {
            items = Json.JsonToKeyedDictionary(json);
        }

    }

    public class DisplayMenu : Display, IDisplay
    {
        public DisplayMenu(string responseJson) : base(responseJson)
        {
            Debug.WriteLine($"Creating menu display from ({responseJson.Length} bytes JSON)");
            Dto dto = new Dto();
            dto.FromJson(responseJson);
        }

        public string GetMenuHtml()
        {
            string html = "<div><b>MENU</b></div>";

            Dto dtoAbfFolder = new Dto(items["AbfFolder"]);
            html += dtoAbfFolder.GetHtml();

            return html;
        }

        public string GetHTML()
        {
            string html = "";
            html += "<html><body>";
            html += GetMenuHtml();
            html += "</body></html>";
            html = Html.Prettify(html);
            return html;
        }

        public string GetText()
        {
            return "menu text";
        }
    }
}
