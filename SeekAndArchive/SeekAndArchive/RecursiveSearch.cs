using System.Collections.Generic;
using System.IO;

namespace SeekAndArchive
{
    partial class SearchPatternFromCanvas
    {
        static void RecursiveSearch(List<FileInfo> Foundfiles, string fileName, DirectoryInfo currentDirectory)
        {
            foreach (FileInfo file in currentDirectory.GetFiles())
            {
                if (file.Name.Contains(fileName))
                {
                    Foundfiles.Add(file);
                }
            }
            foreach (DirectoryInfo directory in currentDirectory.GetDirectories())
            {
                RecursiveSearch(Foundfiles, fileName, directory);
            }
        }
    }
}
