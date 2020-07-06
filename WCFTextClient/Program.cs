using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFTextClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = @"..\..\..\SampleFile.bin";
            if (!File.Exists(file))
            {
                Random rnd = new Random();
                var buffer = new byte[62 * 1024 * 1024];
                rnd.NextBytes(buffer);
                File.WriteAllBytes(file, buffer);
            }
            var f = File.ReadAllBytes(file);
            var watch = Stopwatch.StartNew();
            for (int i = 0; i < 10; i++)
            {
                watch.Restart();
                var service = new ServiceReference1.ServiceClient();
                var result = service.GetData(f);
                Console.WriteLine($"{result}: {watch.ElapsedMilliseconds}ms");
            }
        }
    }
}
