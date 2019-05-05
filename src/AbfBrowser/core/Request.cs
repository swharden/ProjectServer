using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Script.Serialization;

namespace AbfBrowser
{

    public class Request
    {
        private readonly RequestAction action;
        public readonly string path;
        public readonly string identifier;
        public readonly string value;
        public string actionString { get { return action.ToString(); } }

        private readonly JavaScriptSerializer serializer = new JavaScriptSerializer();

        public Request(RequestAction action, string path = null, string identifier = null, string value = null)
        {
            this.action = action;
            this.path = path;
            this.identifier = identifier;
            this.value = value;
            Debug.WriteLine($"building request for action: {action}");
        }

        public Request(string json) //TODO
        {
            throw new NotImplementedException();
        }

        public string LoadJson(string json) //TODO
        {
            throw new NotImplementedException();
        }

        public string GetJson(bool pretty = true)
        {
            Debug.WriteLine("converting Request to JSON");
            string json = serializer.Serialize(this);
            if (pretty)
                json = Json.prettify(json);
            return json;
        }
    }
}