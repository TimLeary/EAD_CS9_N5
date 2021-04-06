using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace CCQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            //Producer: Client sending request to web service and server storing the request in queue.
            IProducerConsumerCollection<string> concurrentQueue = new ConcurrentQueue<string>();            
            Task t1 = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 10; ++i)
                {
                    concurrentQueue.TryAdd("Web request " + i);
                    Console.WriteLine("Sending "+ "Web request " + i);
                    Thread.Sleep(100);
                }
            });
            
            /*
            BlockingCollection<string> blockingCollection = new BlockingCollection<string>(new ConcurrentQueue<string>(),5);    
            Task t1 = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 10; ++i)
                {
                    blockingCollection.TryAdd("Web request " + i);
                    Console.WriteLine("Sending " + "Web request " + i);
                    Thread.Sleep(100);
                }
                blockingCollection.CompleteAdding();
            });
            */
            
            Task t2 = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (concurrentQueue.TryTake(out string request))
                    {
                        Console.WriteLine("Processing "+ request);
                    }
                    else
                    {
                        Console.WriteLine("No request");
                    }
                }
            });
            /*
            Task t2 = Task.Factory.StartNew(() =>
            {
                while (!blockingCollection.IsCompleted)
                {
                    if (blockingCollection.TryTake(out 
                        string request,100))
                    {
                        Console.WriteLine("Processing " + 
                                          request);
                    }
                    else
                    {
                        Console.WriteLine("No request");
                    }
                }
            });
            */
            
            try
            {                
                Task.WaitAll(new Task[] { t1, t2 }, 1000);
            }
            catch (AggregateException ex) // No exception
            {
                Console.WriteLine(ex.Flatten().Message);
            }
        }
    }
}