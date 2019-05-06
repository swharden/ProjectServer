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
        public readonly MessageRequest request;
        public Display displayer;
        public MessageResponse response;

        public Interactor(MessageRequest request)
        {
            Debug.WriteLine("starting Interactor from a MessageRequest object");
            this.request = request;
        }

        public Display Execute(string displayHint = null)
        {
            Debug.WriteLine($"beginning execution of {request.action} Request");
            response = new MessageResponse(request);

            // absent actions are added messageRequest sometimes
            switch (request.action)
            {
                case (RequestAction.doNothing):
                    Debug.WriteLine($"Doing nothing...");
                    break;
                case (RequestAction.scanFolderFast):
                    Debug.WriteLine($"Scanning ABF folder (filenames only)...");
                    response.AbfFolder = new AbfFolder(request.path);
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
                    throw new Exception($"Unimplimented action: {request.action}");
            }

            Debug.WriteLine($"Display hint: {displayHint}");
            switch (displayHint)
            {
                case "home":
                    displayer = new DisplayHome(response);
                    break;
                case "frames":
                    displayer = new DisplayFrames(response);
                    break;
                case "menu":
                    displayer = new DisplayMenu(response);
                    break;
                case "cell":
                    displayer = new DisplayCell(response);
                    break;
                default:
                    displayer = new DisplayError(response);
                    break;
            }

            Debug.WriteLine($"using displayer: {displayer}");
            response.StopwatchStop();
            Debug.WriteLine($"execution completed in {response.elapsedMillisecString} ms");
            return displayer;
        }
    }
}
