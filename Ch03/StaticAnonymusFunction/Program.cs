using System;

namespace StaticAnonymusFunction
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
        
        string formatString = "{0}-{1}";
        
        void GenerateSummary(string[] args)
        {
            GenerateOrderReport(() =>
            {
                return formatString;
            });
        }
        static string GenerateOrderReport(Func<string> getFormatString)
        {
            var order = new
            {
                Orderid = 1,
                OrderDate = DateTime.Now
            };
            return string.Format(getFormatString(), order.Orderid);
        }
    }
}