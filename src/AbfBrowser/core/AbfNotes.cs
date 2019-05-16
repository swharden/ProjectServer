using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbfBrowser
{
    public class AbfNotes
    {
        public List<AbfInfo> abfInfos = new List<AbfInfo>();

        public AbfNotes()
        {
        }

        public AbfNotes(string cellsFilePath)
        {
            if (System.IO.Path.GetFileName(cellsFilePath) == "cells.txt")
                LoadCellsFile(cellsFilePath);
        }

        public string GetComment(string abfID)
        {
            foreach(AbfInfo info in abfInfos)
                if (info.abfID == abfID)
                    return info.comment;
            return "";
        }
        public string GetColorHex(string abfID)
        {
            foreach (AbfInfo info in abfInfos)
                if (info.abfID == abfID)
                    return info.colorHex;
            return Configuration.GetColor("");
        }

        public string GetJson()
        {
            string json = "";
            foreach(AbfInfo info in abfInfos)
            {
                json += info.GetJson();
            }
            json = "{\"abfInfos\":[" + json.Trim(',') + "]}";
            //json = Json.Prettify(json);
            return json;
        }

        public void LoadCellsFile(string cellsFilePath)
        {
            if (System.IO.File.Exists(cellsFilePath))
            {
                Debug.WriteLine($"reading abf folder info file: [{cellsFilePath}]");
                abfInfos.Clear();
                string[] cellsFileLines = System.IO.File.ReadAllLines(cellsFilePath);
                string group = "no group";
                foreach (string cellsFileLine in cellsFileLines)
                {
                    if (cellsFileLine.StartsWith("---"))
                        group = cellsFileLine.Replace("---", "").Trim();
                    AbfInfo abfInfo = new AbfInfo(group: group);
                    abfInfo.LoadCellsFileLine(cellsFileLine);
                    if (abfInfo.IsValid())
                        abfInfos.Add(abfInfo);
                }
            }
            else
            {
                throw new ArgumentException($"abf folder info file does not exist: [{cellsFilePath}]");
            }
        }

        public List<AbfInfo> GetAbfInfo()
        {
            return abfInfos;
        }
    }
}
