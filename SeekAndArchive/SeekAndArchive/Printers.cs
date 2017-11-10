using System;
using System.IO;

namespace SeekAndArchive
{
    partial class SearchPatternFromCanvas
    {
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
            Environment.Exit(0);
        }
    }
}
