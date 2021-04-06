using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskBasics
{
    class Program
    {
        static void Main(string[] args)
        {
            Task dataTask = new Task(
                () => FetchDataFromApi("https://wwww.microsoft.com/en-in/"));
            dataTask.Start();
            dataTask.Wait();
            
            // Task t = Task.Factory.StartNew(delegate { FetchDataFromApi("https://foo.com");});
            // t.Wait();

            // Task dataTask = Task.Run(() => FetchDataFromApi("https://www.microsoft.com/en-in/"));
            // dataTask.Wait();

            // Task t = Task.Factory.StartNew(() => FetchDataFromApi("https://www.microsoft.com/en-in/"));
            // t.Wait();
        }

        public static void FetchDataFromApi(string apiUrl)
        {
            Thread.Sleep(2000);
            Console.WriteLine("Data returned from API");
        }
    }
}