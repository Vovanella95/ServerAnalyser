using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace ZipAnalyser
{
    public class Reader
    {
        public static void Lalala()
        {
            string zipPath = @"C:\Users\Uladzimir_Paliukhovi\Desktop\PackedHeaders\00\0000.zip";

            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    
                }
            } 

        }
        

    }
}
