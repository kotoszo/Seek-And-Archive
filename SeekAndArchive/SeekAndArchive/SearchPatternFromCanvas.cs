using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeekAndArchive
{
    class SearchPatternFromCanvas
    {
        static List<FileInfo> FoundFiles;
        static List<FileSystemWatcher> watchers;

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
            Console.ReadKey();
        }

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

        static void AddFileChangeHandler(FileInfo file)
        {
            FileSystemWatcher newWatcher = new FileSystemWatcher(file.DirectoryName, file.Name);
            newWatcher.Changed += new FileSystemEventHandler(WatcherChanged);
            newWatcher.EnableRaisingEvents = true;
            watchers.Add(newWatcher);
        }

        static void WatcherChanged(object sender, FileSystemEventArgs e)
        {
            if(e.ChangeType == WatcherChangeTypes.Changed)
            {
                Console.WriteLine("{0} has changed.", e.FullPath);
            }
        }

        static void Printer()
        {
            Console.WriteLine("Found {0} files.", FoundFiles.Count);
            foreach (FileInfo fil in FoundFiles)
            {
                Console.WriteLine("{0}", fil.FullName);
            }
        }

        private static void ErrorMessages(int errorIndex)
        {
            switch (errorIndex)
            {
                case 0:
                    Console.WriteLine("1.: Filename\n2.: Directoryname");
                    goto default;
                case 1:
                    Console.WriteLine("There was a problem with the given path.");
                    goto default;
                default:
                    Console.WriteLine("\nPlease, try again.");
                    break;
            }
            System.Environment.Exit(0);
        }

        static void Each<T>(IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
                action(item);
        }
    }
}
