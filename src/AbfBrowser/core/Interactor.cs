using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace AbfBrowser
{
    public class Interactor
    {
        MessageRequest request;

        public Interactor(string requestJson)
        {
            Debug.WriteLine("starting Interactor from a MessageRequest object");
            request = new MessageRequest();
            request.FromJson(requestJson);
        }

        public MessageResponse Execute()
        {
            Debug.WriteLine($"beginning execution of {request.Get("messageType")} message");

            MessageResponse response = new MessageResponse(request);

            string requestActionString = request.Get("action");
            RequestAction requestAction;
            if (!Enum.TryParse(requestActionString, true, out requestAction))
                throw new Exception($"invalid action: {requestActionString}");
            else
                Debug.WriteLine($"switching on requestedAction: {requestActionString}");

            switch (requestAction)
            {
                case (RequestAction.doNothing):
                    Debug.WriteLine($"Doing nothing...");
                    break;
                case (RequestAction.scanFolderFast):
                    Debug.WriteLine($"Scanning ABF folder (filenames only)...");
                    Debug.Assert(request.Contains("path"));
                    AbfFolder abfFolder = new AbfFolder(request.Get("path"));
                    response.Set("AbfFolder", abfFolder.GetJson());
                    break;
                case (RequestAction.scanFolderFull):
                    Debug.WriteLine($"Scanning ABF folder (and text files)...");
                    break;
                case (RequestAction.modifyCell):
                    Debug.WriteLine($"Modifying a cell...");
                    break;
                case (RequestAction.modifyExperiment):
                    Debug.WriteLine($"Modifying an experiment...");
                    break;
                case (RequestAction.analyzeAbf):
                    Debug.WriteLine($"Analyzing ABF(s)...");
                    break;
                case (RequestAction.analyzeTif):
                    Debug.WriteLine($"analyzing TIF(s)");
                    break;
                default:
                    throw new Exception($"Unimplimented action: {requestAction}");
            }

            response.StopwatchStop();
            Debug.WriteLine($"execution completed in {response.StopwatchTimeMsecString()} ms");
            return response;
        }
    }
}
