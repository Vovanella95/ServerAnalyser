using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Net;


using System.Threading;



namespace ZipExtractor
{
    public class Analyser
    {
        public static IEnumerable<string> GetAllContents(string rootPath)
        {
            return (new DirectoryInfo(rootPath))
                .GetDirectories()
                .Select(w => w.FullName)
                .SelectMany((w => (new DirectoryInfo(w))
                    .GetFiles()
                    .Select(ww => ww.FullName)))
                .SelectMany(GetZipElements);
        }

        public static IEnumerable<string> GetZipElements(string zipFileName)
        {
            return ZipFile.OpenRead(zipFileName)
                    .Entries
                    .Select(ww => GetServerHeader(ww.Open()))
                    .Where(cont => cont!=null);
        }

        public static string GetServerHeader(Stream httpHeaders)
        {
            var stream = new StreamReader(httpHeaders);
            while (!stream.EndOfStream)
            {
                var line = stream.ReadLine();
                if (line != null && line.Contains("Server:")) return line;
            }
            return null;
        }

        public static string Analyse(IEnumerable<string> e, string serverName)
        {
            var name = GetName(serverName);
            var version = GetVersion(serverName);
            var info = "На данном сервере стоят " + 100.0*e.Count(w => GetName(w) == name)/e.Count() + "% серверов\n";
            if (version == string.Empty)
            {
                return info;
            }
            info += "Серверов именно этой версии: " +
                    (100.0*e.Count(w => GetName(w) == name && GetVersion(w) == version)/e.Count(w => GetName(w) == name)) +
                    "% всех серверов " + name;
            return info;
        }

        public static string GetVersion(string serverName)
        {
            char[] versionSymbols = new char[]{'1','2','3','4','5','6','7','8','9','.','0'};
            return new string(serverName.Where(w =>versionSymbols.Contains(w)).ToArray());
        }

        public static string GetName(string serverName)
        {
            return serverName.Replace("Server: ","").Replace("\r","").Split('\\', '/', '(',' ').First();
        }
    }
}
