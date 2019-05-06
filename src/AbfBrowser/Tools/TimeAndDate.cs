using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbfBrowser
{
    public static class TimeAndDate
    {
        public static string DateTimeStamp(string separator="T")
        {
            //return DateTime.Now.ToString($"yyyy'-'MM'-'dd'{separator}'HH':'mm':'ss.fff");
            return DateStamp() + separator + TimeStamp();
        }

        public static string TimeStamp()
        {
            return DateTime.Now.ToString("HH':'mm':'ss.fff");
        }

        public static string DateStamp()
        {
            return DateTime.Now.ToString($"yyyy'-'MM'-'dd");
        }
    }
}
