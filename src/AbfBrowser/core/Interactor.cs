using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbfBrowser
{
    public class Interactor
    {
        private readonly Request request;
        private readonly Response response;

        public Interactor(Request request)
        {
            Debug.WriteLine($"constructing Response with {request}");
            this.request = request;
            this.response = new Response(request);
        }

        public Response Execute()
        {
            Debug.WriteLine($"beginning execution");

            Response response = new Response(request);
            System.Threading.Thread.Sleep(100); // simualte
            response.StopwatchStop();
            Debug.WriteLine($"execution completed in {response.executionTimeMsecString} ms");

            return response;
        }
    }
}
