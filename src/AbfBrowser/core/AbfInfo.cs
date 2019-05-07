using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbfBrowser
{
    public class AbfInfo
    {
        public string abfID;
        public string colorCode;
        public string comment;
        public string group;

        public string colorHex => Configuration.GetColor(colorCode);

        public AbfInfo(string abfID = "", string colorCode = "", string comment = "", string group = "no group")
        {
            this.abfID = abfID.Trim();
            this.colorCode = colorCode.Trim();
            this.comment = comment.Trim();
            this.group = group.Trim();
        }

        public bool IsValid()
        {
            if (abfID == null || abfID.Length == 0)
                return false;
            else if (colorCode == null || comment == null || group == null)
                return false;
            else
                return true;
        }

        public void LoadCellsFileLine(string cellsLine)
        {
            cellsLine = cellsLine.Trim();

            if (cellsLine.StartsWith("#") || cellsLine.StartsWith("---") || cellsLine.Length == 0 || !cellsLine.Contains(' '))
                return;

            string[] parts = cellsLine.Split(new char[] { ' ' }, 3);
            if (parts.Length != 3)
                return;

            abfID = parts[0];
            colorCode = parts[1];
            comment = parts[2].Trim();

            if (comment == "?")
                comment = "";
        }

        public string GetCellsFileLine()
        {
            return $"{abfID} {colorCode} {comment}";
        }

        public string GetJson()
        {
            string json = "";
            json += $"\"abfID\":\"{abfID}\",";
            json += $"\"group\":\"{group}\",";
            json += $"\"colorCode\":\"{colorCode}\",";
            json += $"\"colorHex\":\"{colorHex}\",";
            json += $"\"comment\":\"{comment}\",";
            json = "{" + json.Trim(',') + "}";
            return json;
        }
    }
}
