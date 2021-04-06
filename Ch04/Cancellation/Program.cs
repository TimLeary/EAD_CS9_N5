using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cancellation
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cts = new ();
            CancellationToken token = cts.Token;
            Task<string> dataFromAPI;

            try
            {
                dataFromAPI = Task.Factory.StartNew<string>(
                    () => FetchDataFromApi(
                        new List<string> {
                            "https://foo.com",
                            "https://foo1.com",
                            "https://foo2.com",
                            "https://foo3.com",
                            "https://foo4.com",
                        }, token));
                Thread.Sleep(3000);
                cts.Cancel(); //Trigger cancel notification to cancellation token
                dataFromAPI.Wait(); // Wait for task completion
                Console.WriteLine(dataFromAPI.Result); // If task is completed display message accordingly
            }
            catch (AggregateException agex)
            {
                Console.WriteLine(agex);
            }
            
            /*
            dataFromAPI = Task.Factory.StartNew(
                () => FetchDataFromAPIWithCancellation(new 
                    List<string>
                    {
                        "https://foo.com",
                        "https://foo1.com",
                        "https://foo2.com",
                        "https://foo3.com",
                        "https://foo4.com",
                    }, token)).Result;
            */
        }

        public static string FetchDataFromApi(IEnumerable<string> apiUrl, CancellationToken token)
        {
            Console.WriteLine("Task started");
            int counter = 0;
            foreach (string url in apiUrl)
            {
                if (token.IsCancellationRequested)
                {
                    throw new TaskCanceledException($"data from API returned up to iteration {counter}");
                    //throw new OperationCanceledException($"data from API returned up to iteration {counter}"); // Alternate exception with same result
                    //break; // To handle manually 
                }
                Thread.Sleep(1000);
                Console.WriteLine($"data retrieved from {url} for iteration {counter}");
                counter++;
            }
            return $"data from API returned up to iteration {counter}";
        }
        
        public static string FetchDataFromApiWNoToken(IEnumerable<string> apiUrl)
        {
            Console.WriteLine("Task started");
            int counter = 0;
            foreach (string url in apiUrl)
            {
                Thread.Sleep(1000);
                Console.WriteLine($"data retrieved from {url} for iteration {counter}");
                counter++;
            }
            return $"data from API returned up to iteration {counter}";
        }
        
        // For long tasks
        private static Task<string> FetchDataFromAPIWithCancellation(
            IEnumerable<string> apiUrl,
            CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<string>();
            tcs.TrySetCanceled(cancellationToken);
            // calling overload of long running operation that doesn't support cancellation token
            var dataFromAPI = Task.Factory.StartNew(() => FetchDataFromApiWNoToken(apiUrl));
            // Wait for the first task to complete
            var outputTask = Task.WhenAny(dataFromAPI, tcs.Task);
            return outputTask.Result;
        }
    }
}