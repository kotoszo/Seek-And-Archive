using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Security.Permissions;

namespace SeekAndArchive
{
    class Program
    {
        static void mani(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("There was a problem with the arguments.\n" +
                    "1.: Directoryname\n2.:Filename");
                return;
            }
            Run(args);

            /*DirectoryInfo selectedDirectory;
            string fileName;
            FileInfo[] filesToCompress;
            if (args.Length == 1)
            {
                selectedDirectory = new DirectoryInfo(args[0]);
                filesToCompress = selectedDirectory.GetFiles();
            }
            else
            {
                fileName = args[0];
                selectedDirectory = new DirectoryInfo(args[1]);
                filesToCompress = FileSelecter(selectedDirectory.GetFiles(), fileName);
            }

            Compress(filesToCompress, args[1]);*/
        }

        [PermissionSet(SecurityAction.Demand, Name="FullTrust")]
        private static void Run(string[] args)
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = args[1];
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Filter = "*"+args[0]+"*";

            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;
            Console.WriteLine("Press \'q\' to quit.");
            while (Console.Read() != 'q') ;
        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
        }

        private static FileInfo[] FileSelecter(FileInfo[] files, string fileName)
        {
            List<FileInfo> fileList = new List<FileInfo>();
            foreach (var item in files)
            {
                if (item.Name.Contains(fileName)) { fileList.Add(item); }
            }
            return fileList.ToArray();
        }

        public static void Compress(FileInfo[] files, string path)
        {
            using (FileStream compressedFIleStr = File.Create(files[0].Name + ".gz"))
            {
                foreach (var file in files)
                {
                    using(FileStream filedastream = file.OpenRead())
                    {
                        using(GZipStream comprsion = new GZipStream(compressedFIleStr, CompressionMode.Compress))
                        {
                            filedastream.CopyTo(compressedFIleStr);
                        }
                    }
                }

            }


                /*foreach (var fileToCompress in files)
                {
                    using (FileStream originalFileStream = fileToCompress.OpenRead())
                    {
                        using (FileStream compressedFileStream = File.Create(fileToCompress.FullName + ".gz"))
                        {
                            using (GZipStream compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                            {
                                originalFileStream.CopyTo(compressionStream);
                            }
                        }
                    }
                    FileInfo info = new FileInfo(path + "\\" + fileToCompress.Name + ".gz");
                    Console.WriteLine("Compressed {0} from {1} to {2} bytes.",
                            fileToCompress.Name, fileToCompress.Length.ToString(), info.Length.ToString());
                }*/
        }

        public static void Decompress()
        {

        }
    }
}
