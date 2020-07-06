using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace WCFTextCoreClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var file = @"..\..\..\..\SampleFile.bin";
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
                var result = await service.GetDataAsync(f);
                Console.WriteLine($"{result}: {watch.ElapsedMilliseconds}ms");
            }
        }
    }
}
