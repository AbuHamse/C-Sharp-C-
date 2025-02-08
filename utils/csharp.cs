using System;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Entry point of the application
            Console.WriteLine("Hello, World!");

            // Example of a simple menu
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Say Hello");
            Console.WriteLine("2. Exit");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    GreetUser();
                    break;
                case "2":
                    Console.WriteLine("Goodbye!");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }

        static void GreetUser()
        {
            Console.WriteLine("What's your name?");
            string name = Console.ReadLine();
            Console.WriteLine($"Hello, {name}!");
        }
    }
}
