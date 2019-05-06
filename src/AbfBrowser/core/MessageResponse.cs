using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AbfBrowser
{

    public class MessageResponse : Message
    {
        public readonly MessageRequest request;
        public AbfFolder AbfFolder;

        public MessageResponse(MessageRequest request)
        {
            Debug.WriteLine($"Constructing response with request {request}");
            this.request = request;
        }

        public IDisplay GetDisplay()
        {
            return new DisplayMenu(this);
        }
    }
}
