using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intech.App
{
    class MainEntry
    {
        static void Main(string[] args)
        {
            Intech.Business.FileProcessor fprocessor = new Intech.Business.FileProcessor();
            Intech.Business.FileProcessorStatistics result = fprocessor.ProcessFromDirectory("C:\\Windows\\");

            ///! Get Statistics
            Console.WriteLine("TotalFileCount = " + result.TotalFileCount);
            Console.WriteLine("TotalDirectoryCount = " + result.TotalDirectoryCount);
            Console.WriteLine("HiddenFileCount = " + result.HiddenFileCount);
            Console.WriteLine("HiddenDirectoryCount = " + result.HiddenDirectoryCount);
            Console.WriteLine("UnaccessibleFileCount = " + result.UnaccessibleDirectoryCount);

            Console.ReadKey();
        }
    }
}
