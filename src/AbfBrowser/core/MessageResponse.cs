using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AbfBrowser
{

    public class MessageResponse : Dto
    {
        public MessageRequest request;
        public MessageResponse(MessageRequest request)
        {
            Debug.WriteLine($"Constructing response with request {request}");
            this.request = request;
        }
    }
}
