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
        public Tools.Folder analysisFolder;
        public AbfNotes abfNotes = new AbfNotes();

        public AbfFolder(string path) : base(path)
        {
            if (path == null)
                path = "./";
            IdentifyAnalysisFolder();
            DetermineAbfParents();
            IdentifyAndLoadNotesFiles();
        }

        public void IdentifyAndLoadNotesFiles()
        {
            string jsonFilePath = System.IO.Path.Combine(path, Configuration.folderInfoFileName);
            string cellsFilePath = System.IO.Path.Combine(path, Configuration.legacy_cellsFileName);
            if (System.IO.File.Exists(jsonFilePath))
                abfNotes.LoadCellsFile(jsonFilePath);
            else if (System.IO.File.Exists(cellsFilePath))
                abfNotes.LoadCellsFile(cellsFilePath);
        }

        public void IdentifyAnalysisFolder()
        {
            foreach (string analysisFolderName in Configuration.analysisFolderNames)
            {
                string possibleFolderPath = System.IO.Path.Combine(path, analysisFolderName);
                if (System.IO.Directory.Exists(possibleFolderPath))
                {
                    analysisFolder = new Tools.Folder(possibleFolderPath);
                    break;
                }
            }

            if (analysisFolder != null)
                Debug.WriteLine($"analysis folder: [{analysisFolder.path}]");
            else
                Debug.WriteLine($"no analysis folder found");
        }

        private void DetermineAbfParents()
        {
            Debug.WriteLine($"Determining parent/child relationships");

            string[] fileNamesAbf = GetFileNamesEndingWith(".abf");
            string[] fileNamesNonAbf = GetFileNamesNotEndingWith(".abf");
            parentsAndChildren = new Dictionary<string, string[]>();

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

            foreach (string parent in family.Keys)
            {
                string[] children = family[parent].Trim().Split('\n');
                parentsAndChildren[parent] = children;
            }
        }
    }
}
