using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AbfBrowser.Tools
{
    public class Folder
    {
        public readonly string path;
        private string[] fileNames;
        private string[] folderNames;

        public Folder(string path)
        {
            this.path = System.IO.Path.GetFullPath(path);
            Rescan();
        }

        public bool Exists()
        {
            return System.IO.Directory.Exists(path);
        }

        public void Rescan()
        {
            if (!Exists())
            {
                Debug.WriteLine($"Can't scan folder (does not exist): {path}");
                fileNames = null;
                folderNames = null;
            }
            else
            {
                Debug.WriteLine($"Scanning folder: {path}");

                fileNames = System.IO.Directory.GetFiles(path);
                for (int i = 0; i < fileNames.Length; i++)
                    fileNames[i] = System.IO.Path.GetFileName(fileNames[i]);

                folderNames = System.IO.Directory.GetDirectories(path);
                for (int i = 0; i < folderNames.Length; i++)
                    folderNames[i] = System.IO.Path.GetFileName(folderNames[i]);
            }
        }

        public bool FileExists(string fileName)
        {
            if (fileNames == null)
                return false;
            else
                return fileNames.Contains(fileName);
        }

        public bool FolderExists(string folderName)
        {
            if (folderNames == null)
                return false;
            else
                return folderNames.Contains(folderName);
        }

        public string[] GetFileNames()
        {
            return fileNames;
        }

        public string[] GetFolderNames()
        {
            return folderNames;
        }

        public string[] GetFileNamesEndingWith(string extension)
        {
            if (fileNames == null)
                return new string[] { };

            List<string> matchingFilenames = new List<string>();
            foreach (string filename in fileNames)
                if (filename.ToLower().EndsWith(extension.ToLower()))
                    matchingFilenames.Add(filename);
            return matchingFilenames.ToArray();
        }

        public string[] GetFileNamesNotEndingWith(string extension)
        {
            if (fileNames == null)
                return new string[] { };

            List<string> nonMatchingFilenames = new List<string>();
            foreach (string filename in fileNames)
                if (!filename.ToLower().EndsWith(extension.ToLower()))
                    nonMatchingFilenames.Add(filename);
            return nonMatchingFilenames.ToArray();
        }
    }
}
