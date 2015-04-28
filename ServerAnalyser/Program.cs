using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZipExtractor;


namespace ServerAnalyser
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\Uladzimir_Paliukhovi\Desktop\PackedHeaders";
            var a = Class1.GetAllContents(path);
        }
    }
}
