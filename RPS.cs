using System;

class Program
{
    static void Main(string[] args)
    {
        // Define the minimum and maximum values for the random object
        int min = 1; // Rock
        int max = 3; // Scissors

        // Create a random object
        Random random = new Random();

        // Get a random choice for the computer
        int computerChoice = random.Next(min, max + 1);

        // Display options to the user
        Console.WriteLine("Welcome to Rock-Paper-Scissors!");
        Console.WriteLine("Enter your choice: 1 for Rock, 2 for Paper, 3 for Scissors");
        int userChoice;

        // Validate user input
        while (!int.TryParse(Console.ReadLine(), out userChoice) || userChoice < min || userChoice > max)
        {
            Console.WriteLine("Invalid choice. Please enter 1 for Rock, 2 for Paper, or 3 for Scissors:");
        }

        // Display the computer's choice
        string computerChoiceStr = computerChoice switch
        {
            1 => "Rock",
            2 => "Paper",
            3 => "Scissors",
            _ => "Unknown"
        };

        Console.WriteLine($"Computer chose: {computerChoiceStr}");

        // Determine the result
        string result;
        switch (userChoice)
        {
            case 1: // User chose Rock
                result = computerChoice switch
                {
                    1 => "It's a tie!",
                    2 => "You lose! Paper beats Rock.",
                    3 => "You win! Rock beats Scissors.",
                    _ => "Error"
                };
                break;

            case 2: // User chose Paper
                result = computerChoice switch
                {
                    1 => "You win! Paper beats Rock.",
                    2 => "It's a tie!",
                    3 => "You lose! Scissors beats Paper.",
                    _ => "Error"
                };
                break;

            case 3: // User chose Scissors
                result = computerChoice switch
                {
                    1 => "You lose! Rock beats Scissors.",
                    2 => "You win! Scissors beats Paper.",
                    3 => "It's a tie!",
                    _ => "Error"
                };
                break;

            default:
                result = "Error";
                break;
        }

        // Display the result
        Console.WriteLine(result);
    }
}
