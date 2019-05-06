using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AbfBrowser
{
    public class AbfFolder : Tools.Folder
    {
        public Dictionary<string, string[]> parentsAndChildren;
        public string pathAnalysis;

        public Tools.Folder analysisFolder;
        
        public AbfFolder(string path) : base(path)
        {
            if (path == null)
                path = "./";
            pathAnalysis = System.IO.Path.Combine(path, Configuration.analysisFolderName);
            analysisFolder = new Tools.Folder(pathAnalysis);
            DetermineAbfParents();
        }

        public string GetJson()
        {
            return Json.JsonFromObject(this);
        }

        private void DetermineAbfParents()
        {
            Debug.WriteLine($"Determining parent/child relationships");

            string[] fileNamesAbf = GetFileNamesEndingWith(".abf");
            string[] fileNamesNonAbf = GetFileNamesNotEndingWith(".abf");

            // if a non-ABF file starts with the same basename of an ABF, that ABF is a parent.
            List<string> parentAbfList = new List<string>();
            foreach (string fileNameAbf in fileNamesAbf)
                foreach (string fileNameNonAbf in fileNamesNonAbf)
                    if (fileNameNonAbf.StartsWith(System.IO.Path.GetFileNameWithoutExtension(fileNameAbf)))
                        parentAbfList.Add(fileNameAbf);
            string[] fileNamesAbfParents = parentAbfList.ToArray();

            // children are all files alphabetically after the parent (until a new parent is identified)
            Dictionary<string, string> family = new Dictionary<string, string>();
            string currentParent = "orphan";
            foreach(string fileNameChild in fileNamesAbf)
            {
                if (fileNamesAbfParents.Contains(fileNameChild))
                    currentParent = fileNameChild;
                if (!family.ContainsKey(currentParent))
                    family[currentParent] = "";
                family[currentParent] = family[currentParent] + "\n" + fileNameChild;
            }

            parentsAndChildren = new Dictionary<string, string[]>();
            foreach (string parent in family.Keys)
            {
                string[] children = family[parent].Trim().Split('\n');
                parentsAndChildren[parent] = children;
            }
        }
    }
}
