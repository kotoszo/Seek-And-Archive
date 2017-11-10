using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsolatedStorageDemo
{
    class Program
    {
        // yeah, it did something
        // ps: %appdata%

        static void Main(string[] args)
        {
            IsolatedStorageFile userStore = IsolatedStorageFile.GetUserStoreForAssembly();
            IsolatedStorageFileStream userStream = new IsolatedStorageFileStream("UserSettings.set", FileMode.Create, userStore);
            StreamWriter userWriter = new StreamWriter(userStream);
            userWriter.WriteLine("User Prefs");
            userWriter.Close();

            string[] files = userStore.GetFileNames("UserSettings.set");
            if (files.Length == 0)
            {
                Console.WriteLine("nah");
            }

            userStream = new IsolatedStorageFileStream("UserSettings.set", FileMode.Open, userStore);
            StreamReader userReader = new StreamReader(userStream);
            string contents = userReader.ReadToEnd();

            Console.WriteLine(contents);
        }

    }
}
