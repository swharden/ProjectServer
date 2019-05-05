using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbfBrowser
{
    public class Response
    {
        private readonly Request request;
        private readonly System.Diagnostics.Stopwatch stopwatch;

        public Response(Request request)
        {
            System.Diagnostics.Debug.WriteLine($"Constructing response with request {request}");
            this.request = request;
            stopwatch = System.Diagnostics.Stopwatch.StartNew();
        }

        public void StopwatchRestart()
        {
            stopwatch.Restart();
        }

        public void StopwatchStop()
        {
            stopwatch.Stop();
        }

        public double executionTimeMsec
        {
            get { return stopwatch.ElapsedTicks * 1000.0 / System.Diagnostics.Stopwatch.Frequency; }
        }
        public string executionTimeMsecString
        {
            get { return string.Format("{0:0.000}", executionTimeMsec); }
        }

        public string GetJson()
        {
            return "{awesome json response}";
        }
    }
}
