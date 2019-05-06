using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbfBrowser
{
    public class Message
    {
        private readonly Stopwatch stopwatch;

        public readonly string startedTimeDateStamp;
        public double elapsedMillisec { get { return stopwatch.ElapsedTicks * 1000.0 / Stopwatch.Frequency; } }
        public string elapsedMillisecString { get { return string.Format("{0:0.000}", elapsedMillisec); } }

        public Message()
        {
            startedTimeDateStamp = DateTime.Now.ToString($"yyyy'-'MM'-'dd'T'HH':'mm':'ss.fff");
            stopwatch = Stopwatch.StartNew();
        }

        #region stopwatch benchmarking

        public void StopwatchRestart()
        {
            stopwatch.Restart();
        }

        public void StopwatchStop()
        {
            stopwatch.Stop();
        }

        public string GetJson()
        {
            return Json.JsonFromObject(this);
        }

        public void LoadJson(string json)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
