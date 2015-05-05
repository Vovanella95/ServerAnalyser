using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZipExtractor;
using System.Diagnostics;


namespace ServerAnalyser
{
    class Program
    {
        static void Main(string[] args)
        {
            const string path = @"C:\Users\Uladzimir_Paliukhovi\Desktop\PackedHeaders";
            var data = Analyser.GetAllContents(path);
            var c = Analyser.FullAnalyse(data);
            //Class1.Analyse(aaa, "Microsoft-IIS");
           Console.ReadLine();
        }
    }
}
