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
            string inputPath = @"C:\Users\scott\Documents\GitHub\ABF-browser\src\ABF-browser-app\bin\Debug";
            string driveLetter = System.IO.Path.GetPathRoot(inputPath).TrimEnd(new char[] { '/', '\\' });
            List<string> paths = new List<string>();
            List<string> names = new List<string>();
            while (inputPath != null)
            {
                paths.Add(inputPath);
                names.Add(System.IO.Path.GetFileName(inputPath));
                inputPath = System.IO.Path.GetDirectoryName(inputPath);
            }
            paths.Reverse();
            names.Reverse();
            names[0] = driveLetter;

            for (int i = 0; i < paths.Count; i++)
            {
                string line = $"<a href='{paths[i]}'>{names[i]}</a>/";
                Console.WriteLine(line);
            }
        }
    }
}
