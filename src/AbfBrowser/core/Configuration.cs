using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbfBrowser
{
    public class Configuration
    {
        public static string[] analysisFolderNames => new string[] { "swhlab", "_analysis" };
        public static string folderInfoFileName => "abfFolderInfo.json";
        public static string legacy_cellsFileName => "cells.txt";
        public static string legacy_experimentFileName => "experiment.txt";
        public static string version => "1.2";

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

        public static string[] known_protocols = new string[] {
                    "0201 memtest",
                    "0202 IV",
                    "0111 IC ramp",
                    "0113 gain -100 to +500 pA",
                    "0114 gain -500 to +2000 pA"
                };

        public static string GetRandomKnownProtocol(Random rand)
        {
            int knownProtocolCount = known_protocols.Length;
            int randomProtocolIndex = (int)(rand.NextDouble() * knownProtocolCount);
            string randomProtocol = known_protocols[randomProtocolIndex];
            return randomProtocol;
        }
    }
}
