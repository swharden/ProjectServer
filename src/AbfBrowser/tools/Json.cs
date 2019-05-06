using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Script.Serialization;

namespace AbfBrowser
{
    class Json
    {
        public static string JsonFromObject(object obj, bool prettify = true)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(obj);
            if (prettify)
                json = Json.Prettify(json);
            return json;
        }

        public static string Condense(string json)
        {
            string[] lines = json.Replace("\r", "\n").Split('\n');
            string json2 = "";
            foreach (string line in lines)
                json2 += line.Trim();
            Debug.WriteLine($"Condensed {json.Length} bytes of JSON (now {json2.Length} bytes)");
            return json2;
        }
        
        public static string JsonFromKeyedDictionary(Dictionary<string, string> items)
        {
            // order keys by: simple, arrays, sections
            List<string> keys = new List<string>();
            List<string> keysForSections = new List<string>();
            List<string> keysForArrays = new List<string>();
            foreach (KeyValuePair<string, string> pair in items)
            {
                if (pair.Value.StartsWith("{") && pair.Value.EndsWith("}"))
                    keysForSections.Add(pair.Key);
                else if (pair.Value.StartsWith("[") && pair.Value.EndsWith("]"))
                    keysForArrays.Add(pair.Key);
                else
                    keys.Add(pair.Key);
            }
            keys.Sort();
            keysForSections.Sort();
            keysForArrays.Sort();
            keys.AddRange(keysForArrays);
            keys.AddRange(keysForSections);

            // create a mostly-condensed JSON string
            string json = "";
            foreach (string key in keys)
            {
                string value = items[key].Trim();
                if (value.StartsWith("{") && value.EndsWith("}"))
                    json += $"\"{key}\":{value},";
                else if (value.StartsWith("[") && value.EndsWith("]"))
                    json += $"\"{key}\":{value},";
                else
                    json += $"\"{key}\":\"{value}\",";
            }
            json = json.TrimEnd(',');
            json = "{" + json + "}";
            json = Json.Prettify(json);
            return json;
        }

        public static Dictionary<string, string> JsonToKeyedDictionary(string json)
        {
            Dictionary<string, string> items = new Dictionary<string, string>();

            // isolate just the useful lines
            string prettyJson = Json.Prettify(json, 1, '>');
            List<string> goodLines = new List<string>();
            string[] lines = prettyJson.Split('\n');
            foreach (string line in lines)
                if (line.Trim('>').Trim().Length > 0)
                    goodLines.Add(line);
            lines = goodLines.ToArray();

            for (int i = 0; i < lines.Length - 1; i++)
            {
                string line = lines[i].Trim();

                // isolate top-level keys
                if (line.Replace(">", "").Length == 0)
                    continue;
                if (line.StartsWith(">>"))
                    continue;
                int colonPosition = line.IndexOf(":");
                if (colonPosition <= 0)
                    continue;

                string key = line.Substring(1, colonPosition);
                key = key.Trim(':').Trim('"');
                string value = line.Substring(colonPosition, line.Length - colonPosition);
                value = value.Trim(',').Trim(':').Trim('"');

                // if the value is blank, it means second-level data
                if (value == "")
                {
                    if (lines[i + 1].Trim('>').Trim().StartsWith("["))
                    {
                        // second-level data is an array (so make it a string)
                        List<string> arrayValues = new List<string>();
                        i += 2;
                        while (lines[i].Trim().StartsWith(">>\""))
                        {
                            line = lines[i].Trim('>').Trim().Trim(',');
                            arrayValues.Add(line);
                            i += 1;
                        }
                        value = "[" + String.Join(",", arrayValues) + "]";
                    }
                    else if (lines[i + 1].Trim('>').Trim().StartsWith("{"))
                    {
                        // second-level data is JSON (so make it a string)
                        i += 2;
                        string miniJson = "";
                        while (lines[i].Trim().StartsWith(">>"))
                        {
                            line = lines[i].Trim('>').Trim();
                            miniJson += line;
                            i += 1;
                        }
                        value = "{" + miniJson + "}";
                    }
                    else
                    {
                        // just an empty first-level value
                        Console.WriteLine(lines[i]);
                        Console.WriteLine(lines[i + 1]);
                        Console.WriteLine(lines[i + 2]);
                        value = "\"\"";
                    }
                }
                //Console.WriteLine($"{key} = {value}");
                items.Add(key, value);
            }

            Debug.WriteLine($"JSON parsing complete (found {items.Count()} top-level items)");
            return items;
        }

        public static string Prettify(string json, int indentCount = 4, char indentChar = ' ')
        {
            int originalSize = json.Length;
            json = json.Replace("\\,", "~EscapedComma~");
            json = json.Replace(":{", "~ColonStartBracket~\n");
            json = json.Replace("},", "~EndBracketComma~");
            json = json.Replace(":[", "~ColonStartSquareBracket~\n");
            json = json.Replace("],", "~EndSquareBracketComma~");
            json = json.Replace(",", ",\n");
            json = json.Replace("{", "\n{\n");
            json = json.Replace("}", "\n}\n");
            json = json.Replace("[", "\n[\n");
            json = json.Replace("]", "\n]\n");
            json = json.Replace("~ColonStartBracket~", ": \n{");
            json = json.Replace("~EndBracketComma~", "\n},\n");
            json = json.Replace("~ColonStartSquareBracket~", ": \n[");
            json = json.Replace("~EndSquareBracketComma~", "\n],\n");
            json = json.Trim();
            string[] lines = json.Split('\n');

            int bracketsDeep = 0;
            string singleIndent = String.Concat(Enumerable.Repeat(indentChar, indentCount));
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                int indentLevel = bracketsDeep;
                if (line.StartsWith("{") || line.EndsWith("{") || line.StartsWith("[") || line.EndsWith("["))
                {
                    bracketsDeep += 1;
                    indentLevel = bracketsDeep - 1;
                }
                else if (line.EndsWith("}") || line.EndsWith("},") || line.EndsWith("]") || line.EndsWith("],"))
                {
                    bracketsDeep -= 1;
                    indentLevel = bracketsDeep;
                }
                string padding = String.Concat(Enumerable.Repeat(singleIndent, indentLevel));
                lines[i] = padding + line;
            }

            string jsonOut = "";
            foreach (string line in lines)
                if (line.Trim().Length > 0)
                    jsonOut += line + "\r\n";

            Debug.WriteLine($"Prettified {originalSize} bytes of JSON (now {jsonOut.Length} bytes)");
            return jsonOut.Trim();
        }
    }
}
