using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbfBrowser
{
    class Configuration
    {
        public static string[] analysisFolderNames => new string[] { "swhlab", "_analysis" };
        public static string folderInfoFileName => "abfFolderInfo.json";
        public static string legacy_cellsFileName => "cells.txt";
        public static string legacy_experimentFileName => "experiment.txt";

        public static Dictionary<string, string> ColorsByCode => new Dictionary<string, string> {
            {"", "#FFFFFF" },
            {"?", "#EEEEEE"},
            {"g", "#00FF00"},
            {"g1", "#00CC00"},
            {"g2", "#009900"},
            {"b", "#FF9999"},
            {"i", "#CCCCCC"},
            {"s", "#CCCCFF"},
            {"s1", "#9999DD"},
            {"s2", "#6666BB"},
            {"s3", "#333399"},
            {"w", "#FFFF00"},
        };

        public static string GetColor(string colorCode)
        {
            if (ColorsByCode.ContainsKey(colorCode))
            {
                return ColorsByCode[colorCode];
            }
            else
            {
                Debug.WriteLine($"color code [{colorCode}] does not exist (using default color)");
                return ColorsByCode[""];
            }
        }
    }
}
