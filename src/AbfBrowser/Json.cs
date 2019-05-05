using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbfBrowser
{
    class Json
    {
        public static string prettify(string json, int indentation = 2)
        {
            string indentString = String.Concat(Enumerable.Repeat(" ", indentation));
            int indentLevel = 0;
            int quoteCount = 0;
            var result =
                from ch in json
                let quotes = ch == '"' ? quoteCount++ : quoteCount
                let lineBreak = ch == ',' && quotes % 2 == 0 ? ch + Environment.NewLine + String.Concat(Enumerable.Repeat(indentString, indentLevel)) : null
                let openChar = ch == '{' || ch == '[' ? ch + Environment.NewLine + String.Concat(Enumerable.Repeat(indentString, ++indentLevel)) : ch.ToString()
                let closeChar = ch == '}' || ch == ']' ? Environment.NewLine + String.Concat(Enumerable.Repeat(indentString, --indentLevel)) + ch : ch.ToString()
                select lineBreak == null
                            ? openChar.Length > 1
                                ? openChar
                                : closeChar
                            : lineBreak;
            json = String.Concat(result);
            //json = json.Replace("\":", "\" : ");
            return json;
        }
    }
}
