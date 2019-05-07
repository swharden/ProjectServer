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
        readonly Configuration configuration;
        public string url { get { return server.url; } }

        public WebServerManager(bool launch = false)
        {
            configuration = new Configuration();
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
