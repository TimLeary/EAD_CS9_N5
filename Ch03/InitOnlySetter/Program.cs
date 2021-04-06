using System;

namespace InitOnlySetter
{
    class Program
    {
        static void Main(string[] args)
        {
            Order orderObject = new Order
            {
                OrderId = 1,
                TotalPrice = 10.0M
            };

            // orderObject.OrderId = 2 // This will result in compilation error
            Console.WriteLine($"Order: Id: {orderObject.OrderId}, Total price: {orderObject.TotalPrice}");
        }
    }
}