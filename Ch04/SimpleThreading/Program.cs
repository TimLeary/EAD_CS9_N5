using System;
using System.Threading;

namespace SimpleThreading
{
    class Program
    {
        #nullable enable
        
        static void Main(string[] args)
        {
            /* Use Simple Thread  */
            // Thread loadFileFromDisk = new Thread(LoadFileFromDisk);
            // loadFileFromDisk.Start();
            // Thread FetchDataFromApi = new Thread(FetchDataFromApi);
            // FetchDataFromApi.Start("http://dummy/v1/api");
            
            /* Use ThreadPool QueueUserWorkItem instead of Thread */
            ThreadPool.QueueUserWorkItem(LoadFileFromDisk);
            ThreadPool.QueueUserWorkItem(state => FetchDataFromApi("http://dummy/v1/api"));
            
            Console.ReadLine();
        }

        static void FetchDataFromApi(object? apiURL)
        {
            Thread.Sleep(2000);
            Console.WriteLine("Data returned from API");
        }
        
        static void LoadFileFromDisk(object? a)
        {
            Thread.Sleep(2000);
            Console.WriteLine("File loaded from disk");
        }
    }
}