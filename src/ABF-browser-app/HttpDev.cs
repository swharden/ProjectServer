using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABF_browser_app
{
    public static class HttpDev
    {
        private static Random rand = new Random();

        public static string GetTimestamp()
        {
            return DateTime.Now.ToString("HH':'mm':'ss.fff");
        }
        public static string GetDateTimestamp()
        {
            return DateTime.Now.ToString("HH':'mm':'ss.fff");
        }

        public static string GetHtmlPageAutoRefreshingTimestamp()
        {
            string html = @"
                <html>
                <head>
                    <meta http-equiv='refresh' content='.25'> <!-- auto-refresh -->
                    <style>
                        body {
                            background-color: ~bgcolor~;
                        }
                        .consoleHuge{
                            font-family: consolas, courier;
                            font-size: 5em;
                        }
                    </style>
                </head>
                <body>
                    <div class='consoleHuge'>
                        Hello, World!<br>
                        ~timestamp~
                    </div>
                </body>
                </html>".Trim().Replace("                ", "");

            string timestamp = DateTime.Now.ToString("HH':'mm':'ss.fff");
            html = html.Replace("~timestamp~", timestamp);

            string bgcolor = $"rgb({rand.Next(150, 200)},{rand.Next(150, 200)},{rand.Next(150, 200)});";
            html = html.Replace("~bgcolor~", bgcolor);

            return html;
        }
    }
}
