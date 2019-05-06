using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Script.Serialization;

namespace AbfBrowser
{

    public class MessageRequest : Dto
    {
        public MessageRequest(RequestAction action = RequestAction.doNothing, string path = null, string identifier = null, string value = null)
        {
            Debug.WriteLine($"building request for action: {action}");
            Set("messageType", "request");
            Set("action", action.ToString());
            Set("path", path);
            Set("identifier", identifier);
            Set("value", value);
        }

        public MessageRequest()
        {
            Debug.WriteLine($"paramaterless constructor");
        }

        public MessageRequest(string json)
        {
            Debug.WriteLine($"populating items from JSON");
            FromJson(json);
        }
    }
}