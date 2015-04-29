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
            var path = @"C:\Users\Uladzimir_Paliukhovi\Desktop\PackedHeaders2";
            var aaa = Class1.GetAllContents(path);


           /* var ccc = aaa.Where((w => w.Contains("Jetty")));
            int i = 0;
            foreach (var item in ccc)
            {
                Console.WriteLine(item);
                i++;
                if (i > 100)
                {
                    break;
                }
            }*/







            Console.WriteLine(Class1.Analyse(aaa, "Apache(12.12.5)"));
            Console.ReadLine();
        }
    }
}
