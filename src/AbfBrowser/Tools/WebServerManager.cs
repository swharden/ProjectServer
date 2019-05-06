using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbfBrowser
{
    public class WebServerManager
    {
        readonly WebServer server;
        public string url { get { return server.url; } }

        public WebServerManager(bool launch = false)
        {
            server = new WebServer(HttpRequestHandler);
            if (launch)
                Launch();
        }

        public void Launch()
        {
            System.Diagnostics.Process.Start(server.url);
        }

        public static string HttpRequestHandler(System.Net.HttpListenerRequest httpRequest)
        {
            string queryString = httpRequest.RawUrl;

            if (queryString == "/favicon.ico")
                return "";

            if (queryString.StartsWith("/?"))
                queryString = queryString.Substring(2);

            // if a file is requested, serve the file
            string[] fileExtensions = new string[] { ".ico", ".png", ".jpg" };
            foreach (string extension in fileExtensions)
            {
                if (queryString.EndsWith(extension))
                {
                    Debug.WriteLine($"SERVE FILE: {queryString}");
                    return "FILE";
                }
            }

            // determine display hint from display query value
            var queries = System.Web.HttpUtility.ParseQueryString(queryString);
            string displayHint = queries.Get("display");
            if (displayHint == null)
                displayHint = "home";

            // build the request, load it, execute it, and return HTML
            Debug.WriteLine("Debug_Clear");
            MessageRequest request = new MessageRequest(queryString);
            Interactor interactor = new AbfBrowser.Interactor(request);
            Display displayer = interactor.Execute(displayHint);
            string html = displayer.GetHTML();

            return html;
        }

        public string GetLog(bool clear = false)
        {
            return server.GetLog(clear);
        }
    }
}
