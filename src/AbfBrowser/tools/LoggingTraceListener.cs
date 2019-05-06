using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbfBrowser
{

    public class LoggingTraceListener : TraceListener
    {
        private readonly List<string> Log;
        private readonly Stopwatch stopwatch;

        public LoggingTraceListener()
        {
            Log = new List<string>();
            stopwatch = Stopwatch.StartNew();
        }

        public void Clear()
        {
            this.Flush();
            Log.Clear();
            RestartStopwatch();
        }

        public void RestartStopwatch()
        {
            stopwatch.Restart();
        }

        public override void Write(string message)
        {
            throw new NotImplementedException();
        }

        public override void WriteLine(string message)
        {
            message = message.Trim();
            if (message == "Debug_RestartStopwatch")
                RestartStopwatch();
            else if (message == "Debug_Clear")
                Clear();
            else
            {
                double elapsedMsec = stopwatch.ElapsedTicks * 1000.0 / System.Diagnostics.Stopwatch.Frequency;
                string elapsedMsecString = string.Format("{0:000.000}", elapsedMsec);
                string calledFrom = new StackTrace().GetFrame(3).GetMethod().ReflectedType.Name;
                Log.Add($"{elapsedMsecString} ms | {calledFrom}: {message}");
            }
        }

        public string[] GetLogAsArray()
        {
            this.Flush();
            return Log.ToArray();
        }

        public string GetLogAsString(string joiner = "\r\n")
        {
            return string.Join(joiner, GetLogAsArray());
        }
    }
}
