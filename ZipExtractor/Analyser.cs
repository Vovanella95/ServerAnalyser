using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.IO.Compression;




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
            var count = 0;
            var countGlobal = 0;
            var countVersion = 0;
            var versions = new List<string>();

            foreach (var item in e)
            {
                if (GetName(item) == name)
                {
                    countGlobal++;
                    if (GetVersion(item) == version)
                    {
                        countVersion++;
                    }
                    if (GetVersion(item) != "" && !versions.Contains(GetVersion(item)))
                    {
                        versions.Add(GetVersion(item));
                    }
                }
                count++;
            }
            throw new NotImplementedException();
        }

        public static string GetVersion(string serverName)
        {
            var versionSymbols = new char[]{'1','2','3','4','5','6','7','8','9','.','0'};
            return new string(serverName.Where(w =>versionSymbols.Contains(w)).ToArray());
        }

        public static string GetName(string serverName)
        {
            return serverName.Replace("Server: ","").Replace("\r","").Split('\\', '/', '(',' ').First();
        }
    }

    public class VersionInfo
    {
        public string VersionName;
        public int VersionCount;
    }


}
