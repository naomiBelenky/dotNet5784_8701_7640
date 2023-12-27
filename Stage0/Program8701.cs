using System;

namespace Stage0
{
    internal partial class Program
    {
        private static void Main(string[] args)
        {
            Welcome8701();
            Welcome7640();
            Console.ReadKey();
        }

        private static void Welcome8701()
        {
            Console.WriteLine("Enter your name: ");
            string? name = Console.ReadLine();
            Console.WriteLine($"{name}, welcom to my first console application");
        }

        static partial void Welcome7640();
    }
}

