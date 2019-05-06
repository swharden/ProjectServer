using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AbfBrowser
{
    public interface IDisplay
    {
        string GetHTML();

        string GetText();
    }

    public abstract class Display : IDisplay
    {
        public readonly MessageResponse response;

        public Display(MessageResponse response)
        {
            this.response = response;
        }

        public abstract string GetHTML();

        public abstract string GetText();
    }

}
