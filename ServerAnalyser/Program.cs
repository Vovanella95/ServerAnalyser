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
            var c = Analyser.FullAnalyse(data);

            double summ = c.Sum(w => w.ThisCount);

            foreach (var item in c.OrderByDescending(w=>w.ThisCount))
            {
                Console.WriteLine(item.Name + "  -  " + item.ThisCount/summ*100);
            }
           Console.ReadLine();
        }
    }
}
