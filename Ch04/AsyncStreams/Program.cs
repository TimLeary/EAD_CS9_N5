using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncStreams
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var employeeTotal = await GetEmployeeIDAsync(5);
            foreach (int i in employeeTotal)
            {
                Console.WriteLine(i);
            }
        }

        static async Task<IEnumerable<int>> GetEmployeeIDAsync(int input)
        {
            int id = 0;
            List<int> tempId = new List<int>();
            for (int i = 0; i < input; i++) //Some async DB iterator method like ReadNextAsync
            {
                await Task.Delay(1000); // simulate async
                id += i; // Hypothetically calculation
                tempId.Add(id);
            }

            return tempId;
        }
    }
}