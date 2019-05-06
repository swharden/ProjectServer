using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Script.Serialization;

namespace AbfBrowser
{

    public class MessageRequest : Message
    {
        public RequestAction action;
        public string path;
        public string identifier;
        public string value;

        public MessageRequest(RequestAction action = RequestAction.doNothing, string path = null, string identifier = null, string value = null)
        {
            Debug.WriteLine($"Constructing request for action {action}");
            this.action = action;
            this.path = path;
            this.identifier = identifier;
            this.value = value;
        }

        public MessageRequest(string queryString)
        {
            Debug.WriteLine($"Creating MessageRequest from query string: {queryString}");

            System.Collections.Specialized.NameValueCollection queries = System.Web.HttpUtility.ParseQueryString(queryString);

            foreach(string key in queries.Keys)
            {
                string value = queries.Get(key);
                switch (key)
                {
                    case "action":
                        if (Enum.TryParse(value, true, out action))
                            Debug.WriteLine($"action set: {value}");
                        else
                            Debug.WriteLine($"invalid action: {value}");
                        break;
                    case "path":
                        path = value;
                        Debug.WriteLine($"path set: {value}");
                        break;
                    case "identifier":
                        identifier = value;
                        Debug.WriteLine($"identifier set: {value}");
                        break;
                    case "value":
                        this.value = value;
                        Debug.WriteLine($"value set value: {value}");
                        break;
                    default:
                        Debug.WriteLine($"unsure how to handle query: {key}={value}");
                        break;
                }
            }
        }
    }
}