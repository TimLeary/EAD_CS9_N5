using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace CCDictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            // Task t1 as one operation from a client who is adding to the dictionary.
            // With Dictionary it creates AggregateException
            ConcurrentDictionary<int, string> employeeDictionary = new();            
            Task t1 = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 100; ++i)
                {
                    employeeDictionary.TryAdd(i, "Employee" + i.ToString());
                    Thread.Sleep(100);
                    
                }
            });
            
            Task t2 = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(500);
                foreach (var item in employeeDictionary)
                {
                    Console.WriteLine(item.Key + "-" + item.Value);
                    Thread.Sleep(100);
                }
            });

            try
            {
                Task.WaitAll(t1, t2);
            }
            catch (AggregateException e)
            {
                Console.WriteLine(e.Flatten().Message);
            }
        }
    }
}