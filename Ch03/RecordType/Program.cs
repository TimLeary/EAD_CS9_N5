using System;

namespace RecordType
{
    class Program
    {
        static void Main(string[] args)
        {
            Shape s1 = new("Shape");
            Shape s2 = new("Shape");
            
            // ToString of record is overwritten to print the properties of the type
            Console.WriteLine(s1.ToString());
            
            // GetHashCode of record is overwritten to generate the hash code based on values
            Console.WriteLine($"HashCode of s1 is: {s1.GetHashCode()}");
            Console.WriteLine($"HashCode of s2 is: {s2.GetHashCode()}");
            
            // Equality operator of record type is overwritten to check equality based on the values
            Console.WriteLine($"Is s1 equals s2: {s1 == s2}");
            
            
            // Create instance of derived class with the copy of base instance
            Person person = new("David", "Nasztanovics");
            Person person2 = person with {FirstName = "Ferenc"};
        }
    }
    
    public record Shape(string Name);

    public record Person(string FirstName, string LastName);
}