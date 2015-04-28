using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;


namespace ZipExtractor
{
    public class Class1
    {
        public static IEnumerable<string> GetContents(string zipPath)
        {
            ZipArchive archive = ZipFile.OpenRead(zipPath);
            
                return archive.Entries.Select(w =>
                    {
                        var a = w.Open();
                        StreamReader sw = new StreamReader(a);
                        return sw.ReadToEnd();
                    }).Where(w=>w.Contains("Server")).ToList();
        }

        public static IEnumerable<string> GetAllFiles(string rootDirectory)
        {
            string[] dirs = Directory.GetFiles(rootDirectory);
            return dirs;
        }

        public static IEnumerable<string> GetAllDirectories(string root)
        {
            DirectoryInfo a = new DirectoryInfo(root);
            return a.GetDirectories().Select(w => w.FullName);
        }

        public static IEnumerable<string> GetAllContents(string rootPath)
        {
            var directories = GetAllDirectories(rootPath);
            List<string> files = new List<string>();
            foreach (var item in directories)
            {
                files.AddRange(GetAllFiles(item));
            }

            List<string> contents = new List<string>();
            foreach (var item in files)
            {
                contents.AddRange(GetContents(item));
                Console.Clear();
                Console.WriteLine(contents.Count());
            }
            return contents;
        }

        

    }
}
