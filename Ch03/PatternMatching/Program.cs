using System;

namespace PatternMatching
{
    class Program
    {
        static void Main(string[] args)
        {
            Rectangle rectangle = new Rectangle
            {
                Height = 10,
                Width = 0
            };

            if (rectangle.Width is 0)
            {
                Console.WriteLine("The rectangle's width is 0, it will look like a standing line");
            }

            if (rectangle is {Height:0})
            {
                Console.WriteLine("The rectangle's height is 0, it will look like a sleeping line");
            }
            
            // Casting to object to explaining the type pattern
            var newRectangle = (object) new Rectangle
            {
                Height = 10,
                Width = 10
            };

            if (newRectangle is Rectangle rect)
            {
                Console.WriteLine($"The area of rectangle is {rect.Width * rect.Height}");
            }

            if (newRectangle is Rectangle { Height: > 0 and <= 100 })
            {
                Console.WriteLine("This is a rectangle with maximum height of 100");
            }

            Circle circle = new();
            circle.Radius = 10;
            Console.WriteLine($"Area of Circle: {GetAreaOfShape(circle)}");
            
            Rectangle rectangle2 = new();
            rectangle2.Width = 12;
            rectangle2.Height = 8;
            Console.WriteLine($"Area of Rectangle : {GetAreaOfShape(rectangle2)}");
            
            Product product = new Product { Category = "Shirt", Name = "Blue Formal Shirt", Quantity = 10, UnitPrice = 200.00F };
        }
        
        
        /// <summary>
        /// Get the discount of the given product based on the quantity
        /// </summary>
        /// <param name="product">product</param>
        /// <returns>Total discount amount</returns>
        static float GetProductDiscount(Product product)
        {
            float discount = product switch
            {
                { } p when p.Quantity is >= 10 and < 20 => 0.05F,
                { } p when p.Quantity is >= 20 and < 50 => 0.10F,
                { } p when p.Quantity is >= 50 => 0.10F,
                _ => throw new ArgumentException(nameof(product))
            };
            return discount * product.UnitPrice * product.Quantity;
        }
        
        /// <summary>
        /// Get area of the shape by applying right formula.
        /// </summary>
        /// <param name="o">Shape object.</param>
        /// <returns>Area of the shape.</returns>
        static double GetAreaOfShape(object o)
        {
            var result = o switch
            {
                Circle c => (22 / 7.0) * c.Radius * c.Radius,
                Rectangle r => r.Width * r.Height, 
                _ => throw new ArgumentException("Not recognied shape")
            };
            return result;
        }

        static bool AndGate(bool x, bool y) =>
            (x, y) switch
            {
                (false, false) => false,
                (false, true) => false,
                (true, false) => false,
                (true, true) => true
            };

        class Circle
        {
            public double Radius { get; set; }
        }

        class Rectangle
        {
            public double Width { get; set; }
            public double Height { get; set; }
        }

        class Product
        {
            public string Name { get; set; }
            public string Category { get; set; }
            public float UnitPrice { get; set; }
            public int Quantity { get; set; }
        }
    }
}