using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Net;


namespace ZipExtractor
{
    public class Class1
    {
        public static IEnumerable<string> GetAllContents(string rootPath)  // не забыть сделать StreamReader.ReadLine()
        {
            return (new DirectoryInfo(rootPath))
                .GetDirectories()
                .Select(w => w.FullName)
                .SelectMany((w => (new DirectoryInfo(w))
                    .GetFiles()
                    .Select(ww => ww.FullName)))
                    .SelectMany(w =>ZipFile.OpenRead(w)
                    .Entries
                    .Select(ww => (new StreamReader(ww.Open()))
                    .ReadToEnd()).Where(cont => cont.Contains(("Server:")))
                    .Select(fff => fff.Split('\n')
                    .First(ffff => ffff.Contains(("Server:")))));
        }

        public static string Analyse(IEnumerable<string> e, string serverName)
        {
            string name = serverName.Split('\\', '/', '(').First();
            string version = serverName.Substring(name.Length + 1).Replace(")","");

            string[] versionIndex = version.Split('.','\\','/');
            




            throw new NotImplementedException();
        }



    }
}
