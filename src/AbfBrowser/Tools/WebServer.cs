using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using System.Diagnostics;
using System.Net;

namespace AbfBrowser
{
    class WebServer
    {
        private readonly HttpListener listener = new HttpListener();
        private readonly Func<HttpListenerRequest, string> requestHandler;
        public readonly string url;

        public WebServer(Func<HttpListenerRequest, string> requestHandler, string url = "http://localhost:8080/")
        {
            this.url = url;
            this.requestHandler = requestHandler;
            Start();
        }

        public void Start()
        {
            listener.Prefixes.Clear();
            listener.Prefixes.Add(url);
            listener.Start();
            WaitCallback workItemServeForever = new WaitCallback(ListenForever);
            ThreadPool.QueueUserWorkItem(workItemServeForever);
            Log($"WebServer is listening for requests on {url}");
        }

        public void Stop()
        {
            listener.Abort();
            Thread.Sleep(50);
            listener.Close();
            Log($"WebServer stopped listening to requests on: {url}");
        }

        private void ListenForever(Object stateInfo)
        {
            try
            {
                while (listener.IsListening)
                    ThreadPool.QueueUserWorkItem(new WaitCallback(HandleContext), listener.GetContext());
            }
            catch (Exception exception)
            {
                Log(exception);
            }
        }

        private void HandleContext(Object receivedContext)
        {
            HttpListenerContext context = (HttpListenerContext)receivedContext;
            Log($"{context.Request.HttpMethod} {context.Request.Url.PathAndQuery}");
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(requestHandler(context.Request));
                context.Response.ContentLength64 = buffer.Length;
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
            }
            catch (Exception exception)
            {
                Log(exception);
            }
            finally
            {
                context.Response.OutputStream.Close();
            }
        }

        private List<string> logLines = new List<string>();

        public string GetLog(bool clear = false)
        {
            if (clear)
                LogClear();
            return String.Join("\r\n", logLines);
        }

        public void Log(string message)
        {
            if (message.EndsWith("favicon.ico"))
                return;
            string timestamp = DateTime.Now.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss.fff");
            string logLine = $"WebServer [{timestamp}] {message}";
            logLines.Add(logLine);
        }

        public void Log(Exception exception)
        {
            if (exception.Message.Contains("thread exit or an application request"))
            {
                Log("WebServer thread has ended (exception caught)");
            }
            else
            {
                StackTrace trace = new StackTrace();
                int callerIndex = 1;
                StackFrame frame = trace.GetFrame(callerIndex);
                string callerName = frame.GetMethod().Name;
                Log($"EXCEPTION thrown by {callerName}:\n   {exception.ToString()}");
            }
        }

        public void LogClear()
        {
            logLines.Clear();
        }
    }
}
