using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Start");

        await DoSomethingAsync();

        Console.WriteLine("End");
    }

    static async Task DoSomethingAsync()
    {
        Console.WriteLine("Doing something...");
        await Task.Delay(2000); // Simulates a delay like waiting for data
        Console.WriteLine("Done!");
    }
}
