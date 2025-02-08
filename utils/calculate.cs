using System;

class Program
{
    static void Main(string[] args)
    {
        // Input two double values
        Console.Write("Enter the first number (x): ");
        double x = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter the second number (y): ");
        double y = Convert.ToDouble(Console.ReadLine());

        // Perform operations using the methods
        Console.WriteLine($"Addition result: {Add(x, y)}");
        Console.WriteLine($"Subtraction result: {Subtract(x, y)}");
        Console.WriteLine($"Multiplication result: {Multiply(x, y)}");
        Console.WriteLine($"Division result: {Divide(x, y)}");
    }

    // Method to add two doubles
    static double Add(double x, double y)
    {
        return x + y;
    }

    // Method to subtract two doubles
    static double Subtract(double x, double y)
    {
        return x - y;
    }

    // Method to multiply two doubles
    static double Multiply(double x, double y)
    {
        return x * y;
    }

    // Method to divide two doubles
    static double Divide(double x, double y)
    {
        if (y == 0)
        {
            Console.WriteLine("Error: Division by zero is not allowed.");
            return double.NaN; // Return Not-a-Number if division by zero
        }
        return x / y;
    }
}
