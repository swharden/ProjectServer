using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbfBrowser
{
    public class Dto
    {
        private Dictionary<string, string> items = new Dictionary<string, string>();

        private Stopwatch stopwatch;

        public Dto()
        {
            Initialize();
        }
        public Dto(string json)
        {
            FromJson(json);
            Initialize();
        }

        private void Initialize()
        {
            stopwatch = Stopwatch.StartNew();
            StopwatchRestart();
        }

        #region stopwatch benchmarking

        public void StopwatchRestart()
        {
            string stamp = DateTime.Now.ToString($"yyyy'-'MM'-'dd'T'HH':'mm':'ss.fff");
            Set("started", stamp);
            stopwatch.Restart();
            StopwatchUpdateTime(true);
        }

        public void StopwatchUpdateTime(bool forceZero = false)
        {
            if (forceZero)
                Set("elapsedMsec", "0");
            else
                Set("elapsedMsec", StopwatchTimeMsecString());
        }

        public void StopwatchStop()
        {
            stopwatch.Stop();
            StopwatchUpdateTime();
        }
        public double StopwatchTimeMsec()
        {
            return stopwatch.ElapsedTicks * 1000.0 / System.Diagnostics.Stopwatch.Frequency;
        }

        public string StopwatchTimeMsecString()
        {
            return string.Format("{0:0.000}", StopwatchTimeMsec());
        }

        #endregion

        #region accessing data values

        public string Get(string key)
        {
            if (Contains(key))
                return items[key];
            else
                throw new Exception($"key {key} does not exist");
        }

        public void Set(string key, string value)
        {
            items[key] = value;
        }

        public void Set(string key, string[] values)
        {
            string valuesString = "";
            foreach (string value in values)
                valuesString += $"\"{value}\",";
            valuesString = valuesString.TrimEnd(',');
            valuesString = $"[{valuesString}]";
            Set(key, valuesString);
        }

        public bool Contains(string key)
        {
            return items.ContainsKey(key);
        }

        #endregion

        #region data value conversion

        public string GetHtml()
        {
            string html = "";
            foreach (string key in items.Keys)
                html += $"<div>{key} = {items[key]}</div>";
            return html;
        }

        public string GetJson()
        {
            StopwatchUpdateTime();

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

        public void FromJson(string json)
        {
            items = Json.JsonToKeyedDictionary(json);
        }

        #endregion

    }
}
