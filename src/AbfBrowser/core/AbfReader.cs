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
            if (abf.sweepCount == 1)
                msg = msg.Replace("sweeps", "sweep");

            //string msg = "UPDATE VSABF TO GET THIS!";
            string tags = "";
            foreach (var tag in abf.tags)
                tags += string.Format("\"{0}\" at {1:0.0} min, ", tag.comment, tag.timeSec / 60.0);
            tags = tags.Trim().Trim(',');
            if (tags.Length > 0)
                msg += $" TAGS: {tags}";
            return msg;
        }

        public void Dispose()
        {
            abf.Close();
        }
    }
}
