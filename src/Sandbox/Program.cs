using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox
{

    class Program
    {
        static void Main(string[] args)
        {
            var abfnotes2 = new AbfBrowser.AbfNotes(@"D:\demoData\abfs-2019\cells.txt");
            Console.WriteLine(abfnotes2.GetJson());
        }
    }
}
