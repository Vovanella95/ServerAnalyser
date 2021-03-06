﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.IO.Compression;
using System.Net;


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
                if (line != null && line.Split(' ').First()=="Server:") return line;
            }
            return null;
        }

        public static ServerInfo Analyse(IEnumerable<string> e, string serverName)
        {
            var name = GetName(serverName);
            ServerInfo si = new ServerInfo(GetName(serverName),serverName);

            foreach (var item in e)
            {
                if (GetName(item) == name)
                {
                    si.Add(GetVersion(item));
                }
            }
            return si;
        }

        public static IEnumerable<ServerInfo> FullAnalyse(IEnumerable<string> e)
        {
            List<ServerInfo> serverList = new List<ServerInfo>();
            foreach (var item in e)
            {
                if (serverList.Count(w => w.Name == GetName(item)) == 0)
                {
                    serverList.Add(new ServerInfo(GetName(item),item));
                }
                else
                {
                    serverList.First(w=>w.Name==GetName(item)).Add(GetVersion(item));
                }
            }

            return serverList.Where(w=>w.ThisCount>50).ToList();
        }

        public static string GetVersion(string serverName)
        {
            var versionSymbols = new char[]{'1','2','3','4','5','6','7','8','9','.','0'};
            return new string(serverName.Where(w =>versionSymbols.Contains(w)).ToArray());
        }

        public static string GetName(string serverName)
        {
             var name = serverName.Replace("Server: ", "").Split('\\', '/', '(', ' ').First().Replace("\r", "");
            return name.Length > 2 ? name : "Other";
        }

    }

    public class VersionInfo
    {
        public string Name;
        public int Count;
    }

    public class ServerInfo
    {
        public List<VersionInfo> Versions;
        public string Name;
        public string FullName;
        public int ThisCount;
        public ServerInfo(string name, string fullName)
        {
            Name = name;
            FullName = fullName;
            Versions = new List<VersionInfo>();
            ThisCount = 0;
        }
        public void Add(string version)
        {
            if (Versions.Count(w => w.Name == version)==0)
            {
                Versions.Add(new VersionInfo()
                {
                    Name = version,
                    Count = 1
                });
            }
            else
            {
                Versions.First(w=>w.Name==version).Count+=1;
            }
            ThisCount += 1;
        }
    }


}
