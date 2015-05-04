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
            const string path = @"C:\Users\Uladzimir_Paliukhovi\Desktop\PackedHeaders2";
            var data = Analyser.GetAllContents(path);

            Analyser.PrintInfo(Analyser.Analyse(data, "Microsoft-IIS"));
            //Class1.Analyse(aaa, "Microsoft-IIS");
            
            Console.ReadLine();
        }
    }
}
