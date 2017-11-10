using System;
using System.IO;
using System.Collections.Generic;

namespace SeekAndArchive
{
    partial class SearchPatternFromCanvas
    {
        static List<FileInfo> FoundFiles;
        static List<FileSystemWatcher> watchers;
        static List<DirectoryInfo> archiveDirs;

        static void Main(string[] args)
        {
            if (args.Length != 2) { ErrorMessages(0); }

            string fileName = args[0];
            string directoryName = args[1];
            FoundFiles = new List<FileInfo>();
            watchers = new List<FileSystemWatcher>();
            DirectoryInfo rootDir = new DirectoryInfo(directoryName);

            if (!rootDir.Exists) { ErrorMessages(1); }

            RecursiveSearch(FoundFiles, fileName, rootDir);

            Printer();

            Each(FoundFiles, i => AddFileChangeHandler(i));     // DAMN THIS IS GOOD!!!!!!!!!!!!!!!!!!

            archiveDirs = new List<DirectoryInfo>();

            for (int i = 0; i < archiveDirs.Count; i++)
            {
                archiveDirs.Add(Directory.CreateDirectory("archive" + i.ToString()));
            }



            Console.ReadKey();
        }

        

        

        static void Each<T>(IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
                action(item);
        }
    }
}
