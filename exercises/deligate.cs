using System;
using System.IO;

// Delegate definition
public delegate int MathOperation(int x, int y);

class Program
{
    // Two methods to point to with our delegate
    public static int Add(int a, int b) => a + b;
    public static int Multiply(int a, int b) => a * b;

    static void Main()
    {
        Console.WriteLine("Choose operation: 1 = Add, 2 = Multiply");
        string choice = Console.ReadLine();

        // Create the delegate and assign method based on user input
        MathOperation op = choice == "2" ? Multiply : Add;

        // Get numbers from user
        Console.Write("Enter first number: ");
        int x = int.Parse(Console.ReadLine());

        Console.Write("Enter second number: ");
        int y = int.Parse(Console.ReadLine());

        // Use delegate to compute result
        int result = op(x, y);
        Console.WriteLine("Result = " + result);

        // Log result with StreamWriter and shared file access
        string path = Path.Combine(AppContext.BaseDirectory, "results.txt");

        FileStream fs = new FileStream(
            path,
            FileMode.Append,
            FileAccess.Write,
            FileShare.ReadWrite
        );

        using (StreamWriter writer = new StreamWriter(fs))
        {
            writer.WriteLine($"{DateTime.Now}: {x} {(choice == "2" ? "*" : "+")} {y} = {result}");
        }

        Console.WriteLine("Result logged to file.");
    }
}
