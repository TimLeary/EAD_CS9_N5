using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace AsyncAwait
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await DownloadFileAsync("https://github.com/Ravindra-a/largefile/blob/master/README.md",
                $@"{System.IO.Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}download.txt");
            Console.WriteLine("File downloaded!");
        }

        private static async Task DownloadFileAsync(string url, string path)
        {
            using WebClient webClient = new();
            webClient.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; WOW64)");
            byte[] data = await webClient.DownloadDataTaskAsync(url);
            // write data in file
            using var fileStream = File.OpenWrite(path);
            {
                await fileStream.WriteAsync(data, 0, data.Length);
            }
        }
    }
}