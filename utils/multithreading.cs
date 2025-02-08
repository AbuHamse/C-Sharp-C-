using System;
using System.Threading;

class Program
{
    static void PrintNumbers()
    {
        for (int i = 1; i <= 5; i++)
        {
            Console.WriteLine($"Thread 1: {i}");
            Thread.Sleep(500); // Simulate work
        }
    }

    static void PrintLetters()
    {
        for (char c = 'A'; c <= 'E'; c++)
        {
            Console.WriteLine($"Thread 2: {c}");
            Thread.Sleep(500); // Simulate work
        }
    }

    static void Main()
    {
        Thread thread1 = new Thread(PrintNumbers);
        Thread thread2 = new Thread(PrintLetters);

        thread1.Start(); // Start first thread
        thread2.Start(); // Start second thread

        thread1.Join(); // Wait for thread1 to finish
        thread2.Join(); // Wait for thread2 to finish

        Console.WriteLine("Both threads finished.");
    }
}
