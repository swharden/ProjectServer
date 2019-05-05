using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Script.Serialization;

namespace AbfBrowser
{

    public class Request
    {
        public readonly Action action;
        public readonly string path;
        public readonly string identifier;
        public readonly string value;

        private readonly JavaScriptSerializer serializer = new JavaScriptSerializer();

        public Request(Action action, string path = null, string identifier = null, string value = null)
        {
            this.action = action;
            this.path = path;
            this.identifier = identifier;
            this.value = value;
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
            string json = serializer.Serialize(this);
            if (pretty)
                json = Json.prettify(json);
            return json;
        }
    }
}