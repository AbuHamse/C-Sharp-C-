using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Creating a dictionary with string keys and int values
        Dictionary<string, int> ages = new Dictionary<string, int>
        {
            { "Alice", 25 },
            { "Bob", 30 },
            { "Charlie", 35 }
        };

        // Adding a new entry
        ages["David"] = 40;

        // Accessing values
        Console.WriteLine("Alice's age: " + ages["Alice"]);

        // Iterating through dictionary
        foreach (var kvp in ages)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }

        // Checking if a key exists
        if (ages.ContainsKey("Eve"))
        {
            Console.WriteLine("Eve's age: " + ages["Eve"]);
        }
        else
        {
            Console.WriteLine("Eve is not in the dictionary.");
        }
    }
}
