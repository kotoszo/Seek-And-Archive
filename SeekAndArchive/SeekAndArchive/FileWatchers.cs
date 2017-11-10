using System;
using System.IO;

namespace SeekAndArchive
{
    partial class SearchPatternFromCanvas
    {
        static void AddFileChangeHandler(FileInfo file)
        {
            FileSystemWatcher newWatcher = new FileSystemWatcher(file.DirectoryName, file.Name);
            newWatcher.Changed += new FileSystemEventHandler(WatcherChanged);
            newWatcher.EnableRaisingEvents = true;
            watchers.Add(newWatcher);
        }

        // original
        /*static void WatcherChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Changed)
            {
                Console.WriteLine("{0} has changed.", e.FullPath);
            }
        }*/

        static void WatcherChanged(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("{0} has been changed!", e.FullPath);
            FileSystemWatcher senderWatcher = (FileSystemWatcher)sender;
            int index = watchers.IndexOf(senderWatcher, 0);
            ArchiveFile(archiveDirs[index], FoundFiles[index]);
        }
    }
}
