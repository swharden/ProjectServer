using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbfBrowser
{
    public class AbfReader : IDisposable
    {
        public vsABF.ABF abf;

        public AbfReader(string abfFilePath)
        {
            abf = new vsABF.ABF(abfFilePath);
        }

        public string GetOneLineSummary()
        {
            //string msg = $"{abf.channelCount}-channel ABF with {abf.sweepCount} sweeps ({abf.sweepIntervalSec} seconds long)";
            string msg = $"[{abf.protocol}] with {abf.sweepCount} sweeps ({abf.sweepIntervalSec} sec/sweep)";
            return msg;
        }

        public void Dispose()
        {
            abf.Close();
        }
    }
}
